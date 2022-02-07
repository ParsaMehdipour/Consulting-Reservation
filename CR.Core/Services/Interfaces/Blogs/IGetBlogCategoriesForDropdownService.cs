using CR.Core.DTOs.Blogs;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogCategoriesForDropdownService
    {
        List<BlogCategoryForDropdownDto> Execute();
    }
}
