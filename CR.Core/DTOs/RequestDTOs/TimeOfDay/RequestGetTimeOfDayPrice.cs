using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.TimeOfDay
{
    public class RequestGetTimeOfDayPrice
    {
        public long timeOfDayId { get; set; }
        public CallingType callingType { get; set; }
    }
}
