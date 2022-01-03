using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestChangeAppointmentStatusDto
    {
        public long AppointmentId { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string Reason { get; set; }
    }
}
