using System.Collections.Generic;
using CR.Core.DTOs.Appointment;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllAppointmentsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<AppointmentForAdminDto> AppointmentForAdminDtos { get; set; }
    }
}
