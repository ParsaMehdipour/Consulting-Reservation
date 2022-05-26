using System.Collections.Generic;

namespace CR.Core.DTOs.Experts
{
    public class ExpertForSiteDto
    {
        public long expertInformationId { get; set; }
        public string IconSrc { get; set; }
        public string FullName { get; set; }
        public string SpecialitySrc { get; set; }
        public string Speciality { get; set; }
        public decimal AverageRate { get; set; }
        public int RateCount { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public List<string> ClinicImages { get; set; }
        public long? Price { get; set; }
        public bool HasTimeOfDays { get; set; }
        public bool HasStar { get; set; }
        public bool PhoneCall { get; set; }
        public bool VoiceCall { get; set; }
        public bool TextCall { get; set; }
        public List<string> Tags { get; set; }
        public int YearsOfExperience { get; set; }
        public int NumberOfDoneAppointments { get; set; }
    }
}
