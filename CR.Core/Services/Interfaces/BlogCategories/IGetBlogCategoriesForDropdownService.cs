using System.Collections.Generic;
using CR.Core.DTOs.BlogCategories;
using CR.Core.DTOs.Blogs;

namespace CR.Core.Services.Interfaces.BlogCategories
{
    public interface IGetBlogCategoriesForDropdownService
    {
        List<BlogCategoryForDropdownDto> Execute();
    }
}
