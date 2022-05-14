namespace CR.Core.DTOs.Appointments
{
    public class AppointmentForExpertPanelDto
    {
        public long Id { get; set; }
        public long ConsumerId { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string ConsumerFullName { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; }
        public string Degree { get; set; }
        public string AppointmentType { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}
