using System.Collections.Generic;

namespace CR.Core.DTOs.Days
{
    public class RequestEditDayDetaislDto
    {
        public long dayId { get; set; }
        public long expertInformationId { get; set; }
        public List<int> timings { get; set; }
    }
}
