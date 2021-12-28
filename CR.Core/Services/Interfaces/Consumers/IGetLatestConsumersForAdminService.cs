using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetLatestConsumersForAdminService
    {
        ResultDto<List<LatestConsumerForAdminDto>> Execute();
    }
}
