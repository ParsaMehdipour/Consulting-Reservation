using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Wallet
{
    public interface IGetWalletBalanceService
    {
        ResultDto<int> Execute(long payerId);
    }
}
