using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IRemoveTimingService
    {
        ResultDto Execute(long id);
    }
}
