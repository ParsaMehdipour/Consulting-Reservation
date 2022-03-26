using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IGetExpertFinancialTransactionService
    {
        ResultDto<ResultGetExpertFinancialTransactionsDto> Execute(long expertId, int Page = 1, int PageSize = 20);
    }
}
