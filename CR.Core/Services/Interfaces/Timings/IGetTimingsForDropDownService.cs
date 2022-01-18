using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IGetTimingsForDropDownService
    {
        ResultDto<List<TimingDto>> Execute(TimingType timingType);
    }
}
