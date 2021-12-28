using System;
using System.Collections.Generic;

namespace CR.Core.DTOs.ExpertAvailabilities
{
    public class DayDto
    {
        public long id { get; set; }
        public string date_String { get; set; }
        public string dayOfWeek { get; set; }
        public List<TimeOfDayDto> TimeOfDayDtos { get; set; }
    }
}
