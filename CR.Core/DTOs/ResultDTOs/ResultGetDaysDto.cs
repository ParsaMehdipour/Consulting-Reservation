using System.Collections.Generic;
using CR.Core.DTOs.ExpertAvailabilities;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetDaysDto
    {
        public List<DayDto> DayDtos { get; set; }
        public long ExpertInformationId { get; set; }
    }
}
