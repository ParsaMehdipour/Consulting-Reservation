using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IChangeAppointmentStatusService
    {
        ResultDto Execute(long appointmentId, bool activeStatus);
    }
}
