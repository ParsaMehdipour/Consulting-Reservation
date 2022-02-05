using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Consumers;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetConsumerAppointmentsForExpertPanelService
    {
        ResultDto<ResultGetConsumerAppointmentsForExpertPanel> Execute(long expertId,long consumerId,int Page = 1, int PageSize = 20);
    }
}
