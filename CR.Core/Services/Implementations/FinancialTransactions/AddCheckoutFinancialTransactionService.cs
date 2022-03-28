using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class AddCheckoutFinancialTransactionService : IAddCheckoutFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddCheckoutFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long receiverId, int price)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (receiverId == 0 || price == 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مبلغ وارد شده درست نیست"
                    };
                }

                if (_context.FinancialTransactions.Any(_ =>
                        _.ReceiverId == receiverId && _.TransactionType == TransactionType.Checkout &&
                        _.Status == TransactionStatus.UnDefined))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "شما درخواست در در دست بررسی دارید"
                    };
                }

                var sum = _context.FinancialTransactions
                    .Where(_ => _.ReceiverId == receiverId && _.TransactionType == TransactionType.ChargeExpertWallet && _.Status == TransactionStatus.Successful)
                    .Sum(_ => _.Price_Digit);

                var minus = _context.FinancialTransactions
                    .Where(_ => _.ReceiverId == receiverId && _.TransactionType == TransactionType.Checkout && _.Status == TransactionStatus.Successful)
                    .Sum(_ => _.Price_Digit);

                var balance = Convert.ToInt32(sum - minus);

                if (price > balance)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مبلغ تسویه از موجودی کیف پول شما بیشتر می باشد"
                    };
                }

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = _context.Users.FirstOrDefault(_ => _.UserFlag == UserFlag.Admin)!.Id,
                    ReceiverId = receiverId,
                    Price_Digit = price,
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Price_String = price.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    Status = TransactionStatus.UnDefined,
                    TransactionType = TransactionType.Checkout
                };

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "درخواست تسویه با موفقیت ارسال شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
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

