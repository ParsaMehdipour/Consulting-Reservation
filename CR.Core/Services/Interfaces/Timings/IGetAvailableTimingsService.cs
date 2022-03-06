using CR.Common.DTOs;
using CR.Core.DTOs.Timings;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IGetAvailableTimingsService
    {
        ResultDto<AvailableTimingDto> Execute();
    }
}
