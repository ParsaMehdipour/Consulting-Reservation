using System.Collections.Generic;

namespace CR.Core.DTOs.Experts
{
    public class ExpertForSiteDto
    {
        public long expertInformationId { get; set; }
        public string IconSrc { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string SpecialitySrc { get; set; }
        public string Speciality { get; set; }
        public int Rate { get; set; }
        public int RateCount { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public List<string> ClinicImages { get; set; }
        public long? Price { get; set; }
        public bool HasTimeOfDays { get; set; }
        public List<string> Tags { get; set; }
    }
}
