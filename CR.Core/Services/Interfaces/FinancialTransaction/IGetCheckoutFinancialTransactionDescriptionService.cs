using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IGetCheckoutFinancialTransactionDescriptionService
    {
        ResultDto<string> Execute(long id);
    }
}
