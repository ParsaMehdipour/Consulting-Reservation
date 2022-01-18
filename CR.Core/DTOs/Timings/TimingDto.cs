using CR.DataAccess.Enums;

namespace CR.Core.DTOs.Timings
{
    public class TimingDto
    {
        public long Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public TimingType TimingType { get; set; }
    }
}
