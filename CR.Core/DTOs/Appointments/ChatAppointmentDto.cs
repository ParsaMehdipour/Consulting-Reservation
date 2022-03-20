﻿using CR.DataAccess.Enums;
using System;

namespace CR.Core.DTOs.Appointments
{
    public class ChatAppointmentDto
    {
        public CallingType CallingType { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
