using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Timings;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetDayDetailsService
    {
        ResultDto<List<TimingDto>> Execute(long dayId);
    }
}
