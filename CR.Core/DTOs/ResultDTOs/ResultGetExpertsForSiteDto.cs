using System.Collections.Generic;
using CR.Core.DTOs.Experts;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetExpertsForSiteDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ExpertForSiteDto> ExpertForSiteDtos { get; set; }
    }
}
