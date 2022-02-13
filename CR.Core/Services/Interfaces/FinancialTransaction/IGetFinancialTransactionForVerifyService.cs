using CR.Common.DTOs;
using CR.Core.DTOs.FinancialTransactions;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IGetFinancialTransactionForVerifyService
    {
        ResultDto<FinancialTransactionForVerifyDto> Execute(string transactionNumber);
    }
}
