using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAllAppointmentsForExpertPanelService
    {
        ResultDto<ResultGetAllAppointmentsForExpertPanelDto> Execute(long expertId,int Page = 1, int PageSize = 20);
    }
}
