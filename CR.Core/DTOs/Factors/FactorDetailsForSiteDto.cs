﻿using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.Factors
{
    public class FactorDetailsForSiteDto
    {
        public long? expertInformationId { get; set; }
        public string expertIconSrc { get; set; }
        public string expertFullName { get; set; }
        public long price { get; set; }
        public List<AppointmentDetailsForSiteDto> AppointmentDetailsForSiteDtos { get; set; }
    }
}