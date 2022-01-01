namespace CR.Core.DTOs.Appointment
{
    public class AppointmentDetailsForExpertPanel
    {
        public long id { get; set; }
        public string appointmentDate { get; set; }
        public string appointmentTime { get; set; }
        public string appointmentStatus { get; set; }
        public string appointmentPrice { get; set; }
    }
}
