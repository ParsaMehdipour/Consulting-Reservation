namespace CR.Core.DTOs.Appointment
{
    public class AppointmentForAdminDto
    {
        public long Id { get; set; }
        public string ExpertIconSrc { get; set; }
        public string ExpertFullName { get; set; }
        public string Speciality { get; set; }
        public string ConsumerFullName { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentPrice { get; set; }
    }
}
