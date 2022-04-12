using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Appointments;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddDeclineAppointmentConsumerSideFinancialTransactionService
    {
        ResultDto<ResultDeclineAppointmentConsumerSideDto> Execute(long receiverId, long appointmentId);
    }
}
