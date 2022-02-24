using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogDetailsService
    {
        ResultDto<BlogDetailsDto> Execute(long id);
    }
}
