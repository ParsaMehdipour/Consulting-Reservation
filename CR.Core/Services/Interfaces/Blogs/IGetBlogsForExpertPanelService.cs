using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogsForExpertPanelService
    {
        ResultDto<ResultGetBlogsForExpertPanelDto> Execute(long userId, int page = 1, int pageSize = 20);
    }
}
