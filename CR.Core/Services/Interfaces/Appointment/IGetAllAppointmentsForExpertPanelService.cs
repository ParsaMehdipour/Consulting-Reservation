using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Appointments;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAllAppointmentsForExpertPanelService
    {
        ResultDto<ResultGetAllAppointmentsForExpertPanelDto> Execute(long expertId,int Page = 1, int PageSize = 20);
    }
}
