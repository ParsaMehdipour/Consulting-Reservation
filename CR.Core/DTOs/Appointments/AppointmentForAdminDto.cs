namespace CR.Core.DTOs.Appointments
{
    public class AppointmentForAdminDto
    {
        public long Id { get; set; }
        public long ExpertId { get; set; }
        public string ExpertIconSrc { get; set; }
        public string ExpertFullName { get; set; }
        public string Speciality { get; set; }
        public long ConsumerId { get; set; }
        public string ConsumerFullName { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentPrice { get; set; }
        public string AppointmentStatus { get; set; }
        public string CallingType { get; set; }
    }
}
