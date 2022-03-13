using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetExpertCommentsForExpertPanelService
    {
        ResultDto<ResultGetExpertCommentsForExpertPanelDto> Execute(long expertId, int page = 1, int pageSize = 20);
    }
}
