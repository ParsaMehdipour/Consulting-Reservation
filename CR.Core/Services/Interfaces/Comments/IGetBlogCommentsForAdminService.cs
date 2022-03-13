using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetBlogCommentsForAdminService
    {
        ResultDto<ResultGetAllBlogCommentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20);
    }
}
