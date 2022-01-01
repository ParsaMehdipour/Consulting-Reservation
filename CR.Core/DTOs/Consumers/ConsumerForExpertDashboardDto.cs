namespace CR.Core.DTOs.Consumers
{
    public class ConsumerForExpertDashboardDto
    {
        public long ConsumerId { get; set; }
        public long AppointmentId { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string FullName { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string ConsumerType { get; set; }
        public string AppointmentPrice { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
