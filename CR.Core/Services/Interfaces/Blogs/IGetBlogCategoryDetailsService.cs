using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogCategoryDetailsService
    {
        ResultDto<BlogCategoryDetailsForAdminDto> Execute(long id);
    }
}
