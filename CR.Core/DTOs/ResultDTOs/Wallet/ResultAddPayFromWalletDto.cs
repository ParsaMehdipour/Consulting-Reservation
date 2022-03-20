using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Wallet
{
    public class ResultAddPayFromWalletDto
    {
        public long ConsumerId { get; set; }
        public long ExpertInformationId { get; set; }
        public bool IsChat { get; set; }
        public List<ChatAppointmentDto> chatAppointments { get; set; }

    }
}
