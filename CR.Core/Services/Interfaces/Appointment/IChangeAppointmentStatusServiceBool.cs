using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IChangeAppointmentStatusServiceBool
    {
        ResultDto Execute(long appointmentId, bool activeStatus);
    }
}
