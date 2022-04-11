using CR.DataAccess.Enums;

namespace CR.Core.DTOs.ResultDTOs.Appointments
{
    public class ResultChangeAppointmentStatusDto
    {
        public long receiverId { get; set; }
        public int price { get; set; }
        public string phoneNumber { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public AppointmentStatus appointmentStatus { get; set; }
    }
}
