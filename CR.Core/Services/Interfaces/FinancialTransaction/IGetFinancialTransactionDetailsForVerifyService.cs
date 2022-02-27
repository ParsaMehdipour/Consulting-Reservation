using CR.Common.DTOs;
using CR.Core.DTOs.Factors;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IGetFinancialTransactionDetailsForVerifyService
    {
        ResultDto<FactorDetailsForVerifyDto> Execute(string transactionNumber);

    }
}
