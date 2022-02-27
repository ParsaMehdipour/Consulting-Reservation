using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.Factors
{
    public class FactorDetailsForSiteDto
    {
        public long Id { get; set; }
        public long? expertInformationId { get; set; }
        public string expertIconSrc { get; set; }
        public string expertFullName { get; set; }
        public long price { get; set; }
        public string refId { get; set; }
        public long consumerId { get; set; }
        public List<AppointmentDetailsForSiteDto> AppointmentDetailsForSiteDtos { get; set; }
    }
}
