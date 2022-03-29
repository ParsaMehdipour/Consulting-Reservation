using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Payment;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class AddPaymentTransactionService : IAddPaymentTransactionService
    {
        private readonly ApplicationContext _context;

        public AddPaymentTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<RedirectToPaymentForReservationDto> Execute(long factorId, int price)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (factorId == 0 || price == 0)
                {
                    return new ResultDto<RedirectToPaymentForReservationDto>()
                    {
                        Data = new RedirectToPaymentForReservationDto(),
                        IsSuccess = false,
                        Message = "لطفا مبلغ را وارد کنید"
                    };
                }

                var factor = _context.Factors.Include(_ => _.ConsumerInformation).ThenInclude(_ => _.Consumer).FirstOrDefault(_ => _.Id == factorId);

                if (factor == null)
                {
                    return new ResultDto<RedirectToPaymentForReservationDto>()
                    {
                        Data = new RedirectToPaymentForReservationDto(),
                        Message = "فاکتور یافت نشد!!",
                        IsSuccess = false
                    };
                }

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = factor.ConsumerInformation.ConsumerId,
                    Price_Digit = price,
                    ReceiverId = _context.Users.FirstOrDefault(_ => _.UserFlag == UserFlag.Admin)?.Id,
                    Factor = factor,
                    FactorId = factor.Id,
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Price_String = price.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    TransactionType = TransactionType.PayFromCreditCard
                };

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<RedirectToPaymentForReservationDto>()
                {
                    Data = new RedirectToPaymentForReservationDto()
                    {
                        price = Convert.ToInt32(financialTransaction.Price_Digit),
                        transactionNumber = financialTransaction.TransactionNumber,
                        phoneNumber = factor.ConsumerInformation.Consumer.PhoneNumber
                    },
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<RedirectToPaymentForReservationDto>()
                {
                    Data = null,
                    Message = "خطا!!"
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
