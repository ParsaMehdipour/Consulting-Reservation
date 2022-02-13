using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.FinancialTransactions
{
    public class UpdateFinancialTransactionsRefIdService : IUpdateFinancialTransactionsRefIdService
    {
        private readonly ApplicationContext _context;

        public UpdateFinancialTransactionsRefIdService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string transactionNumber, string refId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var financialTransaction =
                    _context.FinancialTransactions.FirstOrDefault(_ => _.TransactionNumber == transactionNumber);

                if (financialTransaction == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تراکنش یافت نشد!!"
                    };
                }

                financialTransaction.RefId = refId;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
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
    }
}
