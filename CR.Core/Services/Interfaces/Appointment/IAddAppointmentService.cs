using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Appointment
{
    public interface IAddAppointmentService
    {
        ResultDto<List<string>> Execute(List<RequestAddAppointmentDto> requests, long consumerId);
    }
}
