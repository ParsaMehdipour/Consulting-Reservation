﻿using System.Collections.Generic;
using CR.Core.DTOs.Appointment;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetConsumerAppointmentsForExpertPanel
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ConsumerAppointmentsForExpertPanelDto> ConsumerAppointmentsForExpertPanelDtos { get; set; }
    }
}
