using CR.Core.DTOs.Appointments;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Wallet
{
    public class ResultAddPayFromWalletDto
    {
        public long ConsumerId { get; set; }
        public long ExpertInformationId { get; set; }
        public bool IsChat { get; set; }
        public string ConsumerPhoneNum { get; set; }
        public string ExpertPhoneNum { get; set; }

        public List<AppointmentDetailsForSMSDto> AppointmentDetailsForConsumerSmsDtos { get; set; }
        public List<AppointmentDetailsForSMSDto> AppointmentDetailsForExpertSmsDtos { get; set; }
        public List<ChatAppointmentDto> chatAppointments { get; set; }

    }
}
