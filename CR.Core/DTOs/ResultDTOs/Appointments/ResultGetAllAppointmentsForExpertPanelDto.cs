﻿using System.Collections.Generic;
using CR.Core.DTOs.Appointments;

namespace CR.Core.DTOs.ResultDTOs.Appointments
{
    public class ResultGetAllAppointmentsForExpertPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<AppointmentForExpertPanelDto> AppointmentForExpertPanelDtos { get; set; }
    }
}
