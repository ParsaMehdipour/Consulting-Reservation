using System.Collections.Generic;

namespace CR.Core.DTOs.Timings
{
    public class TimingForEditDto
    {
        public List<TimingDto> timingDtos { get; set; }
        public string timingTypName { get; set; }
    }
}
