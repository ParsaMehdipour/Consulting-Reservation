using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetBlogCommentsForBlogDetailsService
    {
        ResultDto<List<BlogCommentDto>> Execute(string slug);
    }
}
