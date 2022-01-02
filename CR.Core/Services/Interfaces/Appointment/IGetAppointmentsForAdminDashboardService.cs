using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Appointment;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAppointmentsForAdminDashboardService
    {
        ResultDto<List<AppointmentForAdminDto>> Execute();
    }
}
