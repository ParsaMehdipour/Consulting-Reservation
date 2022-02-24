using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IAddNewCommentService
    {
        ResultDto Execute(RequestAddNewCommentDto request, long userId);
    }
}
