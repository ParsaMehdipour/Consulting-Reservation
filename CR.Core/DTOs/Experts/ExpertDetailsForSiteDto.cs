using System.Collections.Generic;
using CR.Core.DTOs.ExpertAvailabilities;

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForSiteDto
    {
        public long id { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string Speciality { get; set; }
        public string SpecialityImage { get; set; }
        public int Rate { get; set; }
        public int RateCount { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public long? Price { get; set; }
        public string IconSrc { get; set; }

        public List<DayDto> DayDtos { get; set; }
        public string Tag { get; set; }
    }
}
