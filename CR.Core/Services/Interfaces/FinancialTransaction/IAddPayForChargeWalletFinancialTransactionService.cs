using CR.Common.DTOs;
using CR.Core.DTOs.Payment;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddPayForChargeWalletFinancialTransactionService
    {
        ResultDto<RedirectToPaymentForWalletChargeDto> Execute(long payerId, int price);
    }
}
