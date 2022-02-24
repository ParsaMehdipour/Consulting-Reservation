using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetExpertCommentsForAdminPanelService
    {
        ResultDto<ResultGetAllExpertCommentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20);
    }
}
