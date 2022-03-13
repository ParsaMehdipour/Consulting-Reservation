using CR.Core.DTOs.Comments.Blogs;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Comments
{
    public class ResultGetAllBlogCommentsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<BlogCommentForAdminPanelDto> BlogCommentForAdminPanelDtos { get; set; }
    }
}
