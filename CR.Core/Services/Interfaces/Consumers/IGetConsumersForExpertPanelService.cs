using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetConsumersForExpertPanelService
    {
        ResultDto<ResultGetConsumersForExpertPanelDto> Execute(long expertId, int Page = 1, int PageSize = 20);
    }
}
