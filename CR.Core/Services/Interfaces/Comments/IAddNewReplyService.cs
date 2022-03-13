using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IAddNewReplyService
    {
        ResultDto Execute(RequestAddNewReplyDto request, long userId);
    }
}
