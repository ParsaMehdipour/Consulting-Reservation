using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IAddNewTimingService
    {
        ResultDto Execute(RequestAddNewTimingDto request);
    }
}
