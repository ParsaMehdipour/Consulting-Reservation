using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class ChangeCheckoutFinancialTransactionService : IChangeCheckoutFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public ChangeCheckoutFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long transactionId, TransactionStatus status)
        {
            var transaction = _context.FinancialTransactions.Find(transactionId);

            if (transaction == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "اطلاعات تراکنش یافت نشد"
                };
            }

            transaction.Status = status;

            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "وضعیت تراکنش با موفقیت تغیر کرد"
            };
        }
    }
}
