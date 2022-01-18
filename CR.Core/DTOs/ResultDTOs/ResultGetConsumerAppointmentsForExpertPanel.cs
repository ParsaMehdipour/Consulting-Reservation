using System.Collections.Generic;
using CR.Core.DTOs.Appointments;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetConsumerAppointmentsForExpertPanel
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string ConsumerFullName { get; set; }
        public string Age { get; set; }
        public string BloodType { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public List<ConsumerAppointmentsForExpertPanelDto> ConsumerAppointmentsForExpertPanelDtos { get; set; }
    }
}
