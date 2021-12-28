using System;

namespace CR.Core.DTOs.ExpertAvailabilities
{
    public class TimeOfDayDto
    {
        public long id { get; set; }
        public long expertInformationId { get; set; }
        public long dayId { get; set; }
        public string start { get; set; }
        public string finish { get; set; }
    }
}
