using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddDeclineAppointmentFinancialTransactionService
    {
        ResultDto Execute(long receiverId, long appointmentId);
    }
}
