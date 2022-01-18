namespace CR.Core.DTOs.Appointments
{
    public class AppointmentForConsumerPanelDto
    {
        public long Id { get; set; }
        public long ExpertInformationId { get; set; }
        public string ExpertIconSrc { get; set; }
        public string ExpertFullName { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string ReservationDate { get; set; }
        public string Price { get; set; }
        public string ExpertTracking { get; set; }
        public string Status { get; set; }
        public string Speciality { get; set; }
    }
}
