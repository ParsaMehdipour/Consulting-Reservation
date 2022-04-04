using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class GetCheckoutFinancialTransactionDescriptionService : IGetCheckoutFinancialTransactionDescriptionService
    {
        private readonly ApplicationContext _context;

        public GetCheckoutFinancialTransactionDescriptionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<string> Execute(long id)
        {
            var transaction = _context.FinancialTransactions.Find(id);

            if (transaction == null)
            {
                return new ResultDto<string>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "تراکنش یافت نشد"
                };
            }

            var description = (string.IsNullOrEmpty(transaction.Description)) ? "در انتظار تعیین وضعیت" : transaction.Description;

            return new ResultDto<string>()
            {
                Data = description,
                IsSuccess = true
            };
        }
    }
}
