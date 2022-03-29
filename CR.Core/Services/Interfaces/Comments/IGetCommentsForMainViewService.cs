using CR.Common.DTOs;
using CR.Core.DTOs.Comments;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetCommentsForMainViewService
    {
        ResultDto<List<CommentForMainViewDto>> Execute();
    }
}
