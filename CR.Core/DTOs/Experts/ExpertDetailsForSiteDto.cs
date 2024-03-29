﻿using CR.Core.DTOs.Days;
using System.Collections.Generic;

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForSiteDto
    {
        public long id { get; set; }
        public string FullName { get; set; }
        public string Speciality { get; set; }
        public string SpecialityImage { get; set; }
        public int RateCount { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string IconSrc { get; set; }
        public decimal AverageRate { get; set; }
        public int YearsOfExperience { get; set; }
        public int NumberOfDoneAppointments { get; set; }
        public bool HasStar { get; set; }

        public List<DayForExpertDetailsDto> DayDtos { get; set; }
        public List<ExpertMembershipDto> memberships { get; set; }
        public List<ExpertPrizeDto> prizes { get; set; }
        public List<ExpertStudyDto> studies { get; set; }
        public List<ExpertSubscriptionDto> subscriptions { get; set; }
        public string Tag { get; set; }
        public List<string> Tags { get; set; }
    }
}
