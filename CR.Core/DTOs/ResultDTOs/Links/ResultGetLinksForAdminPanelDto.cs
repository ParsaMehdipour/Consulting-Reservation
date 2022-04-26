using CR.Core.DTOs.Links;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Links
{
    public class ResultGetLinksForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<LinkForAdminPanelDto> LinkForAdminPanelDtos { get; set; }
    }
}
