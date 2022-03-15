using CR.Core.DTOs.ExpertAvailabilities;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.ExpertAvailabilities
{
    public class ResultGetExpertAvailabilitiesDetailsDto
    {
        public List<DayDto> dayDtos { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }
}
