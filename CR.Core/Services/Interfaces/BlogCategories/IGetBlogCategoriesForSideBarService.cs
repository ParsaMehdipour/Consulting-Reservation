using CR.Common.DTOs;
using CR.Core.DTOs.BlogCategories;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.BlogCategories
{
    public interface IGetBlogCategoriesForSideBarService
    {
        ResultDto<List<BlogCategoryForSideBarDto>> Execute();
    }
}
