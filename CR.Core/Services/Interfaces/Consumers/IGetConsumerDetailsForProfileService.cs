using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Consumers;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetConsumerDetailsForProfileService
    {
        ResultDto<ResultGetConsumerDetailsForSiteDto> Execute(long consumerId);
    }
}
