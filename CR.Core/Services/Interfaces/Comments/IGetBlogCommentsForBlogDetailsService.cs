using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;
using CR.Core.DTOs.Comments.Blogs;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetBlogCommentsForBlogDetailsService
    {
        ResultDto<List<BlogCommentDto>> Execute(string slug);
    }
}
