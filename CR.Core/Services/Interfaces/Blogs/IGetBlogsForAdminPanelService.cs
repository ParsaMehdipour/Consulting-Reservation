using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogsForAdminPanelService
    {
        ResultDto<ResultGetBlogsForAdminPanelDto> Execute(int page = 1, int pageSize = 20);
    }
}
