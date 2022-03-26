using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddChargeExpertWalletService
    {
        ResultDto Execute(long receiverId, int price);
    }
}
