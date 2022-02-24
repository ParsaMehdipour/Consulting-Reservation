using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Comments
{
    public class ResultGetAllExpertCommentsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ExpertCommentForAdminPanelDto> ExpertCommentForAdminPanelDtos { get; set; }
    }
}
