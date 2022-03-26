using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Wallet
{
    public interface IGetExpertWalletBalanceService
    {
        ResultDto<int> Execute(long receiverId);
    }
}
