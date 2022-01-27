using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddAppointmentDto
    {
        public long timeOfDayId { get; set; }
        public long expertInformationId { get; set; }
        public CallingType callingType { get; set; }
    }
}
