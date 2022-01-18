using System.Collections.Generic;
using CR.Core.DTOs.Appointments;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllAppointmentsForConsumerPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<AppointmentForConsumerPanelDto> AppointmentForConsumerPanelDtos { get; set; }
    }
}
