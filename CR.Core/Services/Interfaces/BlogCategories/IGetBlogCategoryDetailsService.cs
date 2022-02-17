using CR.Common.DTOs;
using CR.Core.DTOs.BlogCategories;
using CR.Core.DTOs.Blogs;

namespace CR.Core.Services.Interfaces.BlogCategories
{
    public interface IGetBlogCategoryDetailsService
    {
        ResultDto<BlogCategoryDetailsForAdminDto> Execute(long id);
    }
}
