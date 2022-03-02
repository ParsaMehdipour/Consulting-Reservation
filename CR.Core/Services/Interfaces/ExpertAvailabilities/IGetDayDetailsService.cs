using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetDayDetailsService
    {
        ResultDto<List<TimingForEditDto>> Execute(long dayId, long expertId);
    }
}
