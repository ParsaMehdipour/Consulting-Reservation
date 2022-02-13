using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.FinancialTransactions
{
    public class UpdateFinancialTransactionCarHolderPAN : IUpdateFinancialTransactionCarHolderPANService
    {
        private readonly ApplicationContext _context;

        public UpdateFinancialTransactionCarHolderPAN(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string transactionNumber, string cardHolderPAN)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var financialTransaction = _context.FinancialTransactions.FirstOrDefault(_ => _.TransactionNumber == transactionNumber);

                if (financialTransaction == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تراکنش یافت نشد!!"
                    };
                }

                financialTransaction.CardHolderPAN = cardHolderPAN;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
        }
    }
}
