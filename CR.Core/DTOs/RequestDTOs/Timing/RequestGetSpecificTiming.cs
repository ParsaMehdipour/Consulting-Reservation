using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Timing
{
    public class RequestGetSpecificTiming
    {
        public TimingType timingType { get; set; }
        public long expertInformationId { get; set; }
        public string date_String { get; set; }
    }
}
