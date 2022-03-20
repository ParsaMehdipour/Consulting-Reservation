using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Factors
{
    public class ResultUpdateFactorStatusDto
    {
        public long ConsumerId { get; set; }
        public long ExpertInformationId { get; set; }
        public bool IsChat { get; set; }
        public List<ChatAppointmentDto> chatAppointments { get; set; }
    }
}
