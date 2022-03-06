using System.Collections.Generic;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewTimeOfDayDto
    {
        public long dayId { get; set; }
        public long expertInformationId { get; set; }
        public List<int> timings { get; set; }
    }
}
