using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class GetFinancialTransactionForVerifyService : IGetFinancialTransactionForVerifyService
    {
        private readonly ApplicationContext _context;

        public GetFinancialTransactionForVerifyService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<FinancialTransactionForVerifyDto> Execute(string transactionNumber)
        {
            var financialTransaction = _context.FinancialTransactions.FirstOrDefault(_ => _.TransactionNumber == transactionNumber);

            if (financialTransaction == null)
            {
                return new ResultDto<FinancialTransactionForVerifyDto>()
                {
                    IsSuccess = false,
                    Message = string.Empty,
                    Data = null
                };
            }

            return new ResultDto<FinancialTransactionForVerifyDto>()
            {
                Data = new FinancialTransactionForVerifyDto()
                {
                    Price = financialTransaction.Price_Digit,
                    RefId = financialTransaction.RefId
                },
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }
}
