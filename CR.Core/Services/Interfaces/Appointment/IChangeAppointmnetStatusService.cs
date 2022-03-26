using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs.Appointments;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IChangeAppointmentStatusService
    {
        ResultDto<ResultChangeAppointmentStatusDto> Execute(RequestChangeAppointmentStatusDto request);
    }
}
