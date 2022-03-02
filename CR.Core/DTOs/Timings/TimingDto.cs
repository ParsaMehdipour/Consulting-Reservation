using CR.DataAccess.Enums;

namespace CR.Core.DTOs.Timings
{
    public class TimingDto
    {
        public long id { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public TimingType timingType { get; set; }
        public bool IsSelected { get; set; }
    }
}
