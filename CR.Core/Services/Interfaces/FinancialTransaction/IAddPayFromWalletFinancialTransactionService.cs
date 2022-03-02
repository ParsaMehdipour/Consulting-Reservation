using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddPayFromWalletFinancialTransactionService
    {
        ResultDto Execute(long payerId, long factorId, int price);
    }
}
