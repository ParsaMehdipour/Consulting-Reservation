using System.Collections.Generic;
using CR.Core.DTOs.Timings;
using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewTimingDto
    {
        public List<TimingDto> timings { get; set; }
    }
}
