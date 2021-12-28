using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IAddAppointmentService
    {
        ResultDto Execute(RequestAddAppointmentDto request);
    }
}
