using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;
using CR.Core.DTOs.Comments.Blogs;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetBlogCommentsForBlogDetailsByIdService
    {
        ResultDto<List<BlogCommentDto>> Execute(long id);

    }
}
