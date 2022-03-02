using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IGetTimingsForDropDownService
    {
        ResultDto<TimingForDropDownDtos> Execute(TimingType timingType);
    }
}
