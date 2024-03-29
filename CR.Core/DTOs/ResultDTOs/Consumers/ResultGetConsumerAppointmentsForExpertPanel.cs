﻿using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Consumers
{
    public class ResultGetConsumerAppointmentsForExpertPanel
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string ConsumerFullName { get; set; }
        public string Age { get; set; }
        public string Degree { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public List<ConsumerAppointmentsForExpertPanelDto> ConsumerAppointmentsForExpertPanelDtos { get; set; }
    }
}
