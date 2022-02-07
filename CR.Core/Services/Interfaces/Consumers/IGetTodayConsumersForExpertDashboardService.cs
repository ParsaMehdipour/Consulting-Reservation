using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Consumers;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetTodayConsumersForExpertDashboardService
    {
        ResultDto<ResultGetConsumersForExpertDashboardDto> Execute(long expertId, int Page = 1, int PageSize = 20);
    }
}
