using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Wallet;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddPayFromWalletFinancialTransactionService
    {
        ResultDto<ResultAddPayFromWalletDto> Execute(long payerId, long factorId, int price);
    }
}
