using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs.Wallet;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class AddPayFromWalletFinancialTransactionService : IAddPayFromWalletFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddPayFromWalletFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultAddPayFromWalletDto> Execute(long payerId, long factorId, int price)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                if (factorId == 0 || price == 0)
                {
                    return new ResultDto<ResultAddPayFromWalletDto>()
                    {
                        IsSuccess = false,
                        Message = "لطفا مبلغ را وارد کنید",
                        Data = new ResultAddPayFromWalletDto()
                    };
                }

                var sum = _context.FinancialTransactions
                    .Where(_ => _.PayerId == payerId && (_.TransactionType == TransactionType.ChargeWallet || _.TransactionType == TransactionType.DeclineTransaction) && _.Status == TransactionStatus.Successful)
                    .Sum(_ => _.Price_Digit);

                var minus = _context.FinancialTransactions
                    .Where(_ => _.PayerId == payerId && _.TransactionType == TransactionType.PayFromWallet && _.Status == TransactionStatus.Successful)
                    .Sum(_ => _.Price_Digit);

                var balance = Convert.ToInt32(sum - minus);

                if (balance < price)
                {
                    return new ResultDto<ResultAddPayFromWalletDto>()
                    {
                        IsSuccess = false,
                        Message = "موجودی کیف پول شما کافی نیست",
                        Data = new ResultAddPayFromWalletDto()
                    };
                }

                var factor = _context.Factors
                    .Include(_ => _.ConsumerInformation)
                    .Include(_ => _.ExpertInformation)
                    .Include(_ => _.Appointments)
                    .ThenInclude(_ => _.TimeOfDay)
                    .FirstOrDefault(_ => _.Id == factorId);

                if (factor == null)
                {
                    return new ResultDto<ResultAddPayFromWalletDto>()
                    {
                        IsSuccess = false,
                        Message = "فاکتور یافت نشد!!",
                        Data = new ResultAddPayFromWalletDto()
                    };
                }

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = payerId,
                    Price_Digit = price,
                    FactorId = factorId,
                    Factor = factor,
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Price_String = price.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    TransactionType = TransactionType.PayFromWallet,
                    Status = TransactionStatus.Successful
                };

                factor.FactorStatus = FactorStatus.SuccessfulPayment;

                foreach (var appointment in factor.Appointments)
                {
                    appointment.TimeOfDay.IsReserved = true;
                }

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<ResultAddPayFromWalletDto>()
                {
                    IsSuccess = true,
                    Message = (factor.Appointments.Any(_ => _.CallingType == CallingType.TextCall || _.CallingType == CallingType.VoiceCall)) ? "تراکنش با موفقیت انجام شد مشاور به لیست کاربران برای ارسال پیام شما اضافه شد" : "تراکنش با موفقیت انجام شد",
                    Data = new ResultAddPayFromWalletDto()
                    {
                        ConsumerId = factor.ConsumerInformation.ConsumerId,
                        ExpertInformationId = factor.ExpertInformation.Id,
                        IsChat = factor.Appointments.Any(_ => _.CallingType == CallingType.TextCall || _.CallingType == CallingType.VoiceCall)
                    }
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<ResultAddPayFromWalletDto>()
                {
                    IsSuccess = false,
                    Message = "خطا!",
                    Data = new ResultAddPayFromWalletDto()
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
        private string GetLastTransactionNumber()
        {
            var financialTransactions = _context.FinancialTransactions.OrderBy(f => f.Id).LastOrDefault();

            if (financialTransactions == null)
            {
                return "1";
            }

            return (Convert.ToInt32(financialTransactions.TransactionNumber) + 1).ToString();
        }
    }
}
