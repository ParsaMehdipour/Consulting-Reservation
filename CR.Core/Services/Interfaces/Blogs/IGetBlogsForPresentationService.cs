using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogsForPresentationService
    {
        ResultDto<ResultGetBlogsForPresentationDto> Execute(string searchKey, int Page = 1, int PageSize = 20);
    }
}
