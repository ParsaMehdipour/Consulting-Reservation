using System.Collections.Generic;
using System.Linq;
using CR.Core.DTOs.Timings;
using CR.DataAccess.Enums;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllTimingsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<TimingDto> TimingDtos { get; set; }
    }
}
