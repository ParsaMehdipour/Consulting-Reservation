using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Appointments;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAllAppointmentsForConsumerPanelService
    {
        ResultDto<ResultGetAllAppointmentsForConsumerPanelDto> Execute(long consumerId, int Page = 1, int PageSize = 20);
    }
}
