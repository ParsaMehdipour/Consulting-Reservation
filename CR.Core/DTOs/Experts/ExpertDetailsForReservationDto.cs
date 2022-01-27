using CR.Core.DTOs.ExpertAvailabilities;
using System;
using System.Collections.Generic;

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForReservationDto
    {
        public long ExpertInformationId { get; set; }
        public string Speciality { get; set; }
        public string SpecialitySrc { get; set; }
        public string FullName { get; set; }
        public long? phonePrice { get; set; }
        public long? voicePrice { get; set; }
        public long? textPrice { get; set; }
        public string IconSrc { get; set; }
        public int Rate { get; set; }
        public int RateCount { get; set; }
        public DateTime? ThisDate { get; set; }
        public List<DayDto> DayDtos { get; set; }

    }
}
