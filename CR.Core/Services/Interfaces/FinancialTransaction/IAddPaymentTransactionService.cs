using CR.Common.DTOs;
using CR.Core.DTOs.Payment;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddPaymentTransactionService
    {
        ResultDto<RedirectToPaymentForReservationDto> Execute(long factorId, int price);
    }
}
