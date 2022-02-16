using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.BlogCategories
{
    public interface IDeleteBlogCategoryService
    {
        ResultDto Execute(long id);
    }
}
