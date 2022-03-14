using CR.Common.DTOs;
using CR.Core.DTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetCommentDetailsForAdminPanelService
    {
        ResultDto<CommentDetailsForAdminPanelDto> Execute(long id);
    }
}
