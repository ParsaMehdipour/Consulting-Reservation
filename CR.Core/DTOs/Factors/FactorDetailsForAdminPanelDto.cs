using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.Factors
{
    public class FactorDetailsForAdminPanelDto
    {
        public string CreateDate { get; set; }
        public string FactorNumber { get; set; }
        public long? expertId { get; set; }
        public string expertIconSrc { get; set; }
        public string expertFullName { get; set; }
        public long price { get; set; }
        public long DiscountPrice { get; set; }
        public long CommissionPrice { get; set; }
        public long RawPrice { get; set; }
        public long consumerId { get; set; }
        public string consumerIconSrc { get; set; }
        public string consumerFullName { get; set; }
        public string FactorStatus { get; set; }
        public List<AppointmentDetailsForSiteDto> AppointmentDetailsForSiteDtos { get; set; }
    }
}
