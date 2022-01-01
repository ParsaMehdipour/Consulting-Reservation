using CR.Common.DTOs;
using CR.Core.DTOs.Appointment;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IGetAppointmentDetailsForExpertPanelService
    {
        ResultDto<AppointmentDetailsForExpertPanel> Execute(long id);
    }
}
