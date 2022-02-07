using System.Collections.Generic;
using CR.Core.DTOs.Timings;

namespace CR.Core.DTOs.ResultDTOs.Timings
{
    public class ResultGetAllTimingsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<TimingDto> TimingDtos { get; set; }
    }
}
