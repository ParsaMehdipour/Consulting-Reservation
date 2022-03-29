using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IShowCommentInMainPageService
    {
        ResultDto Execute(long commentId, bool showStatus);
    }
}
