using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Appointments
{
    public class ResultGetAllAppointmentsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<AppointmentForAdminDto> AppointmentForAdminDtos { get; set; }
    }
}
