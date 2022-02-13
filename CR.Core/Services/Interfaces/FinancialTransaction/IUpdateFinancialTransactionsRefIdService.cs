using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IUpdateFinancialTransactionsRefIdService
    {
        ResultDto Execute(string transactionNumber, string refId);
    }
}
