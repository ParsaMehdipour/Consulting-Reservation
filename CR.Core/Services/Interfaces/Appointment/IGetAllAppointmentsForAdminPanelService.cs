using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAllAppointmentsForAdminPanelService
    {
        ResultDto<ResultGetAllAppointmentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20);
    }
}
