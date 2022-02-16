using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.BlogCategories
{
    public interface IEditBlogCategoryDetailsService
    {
        ResultDto Execute(RequestEditBlogCategoryDto request);
    }
}
