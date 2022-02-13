using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IUpdateFinancialTransactionCarHolderPANService
    {
        ResultDto Execute(string transactionNumber, string cardHolderPAN);
    }
}
