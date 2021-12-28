namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddAppointmentDto
    {
        public long timeOfDayId { get; set; }
        public long expertInformationId { get; set; }
        public long consumerId { get; set; }
    }
}
