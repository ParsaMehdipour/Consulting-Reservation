using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddChargeWalletFinancialTransactionService
    {
        ResultDto Execute(long receiverId, long price);
    }
}
