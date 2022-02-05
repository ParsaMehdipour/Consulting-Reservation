using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IDeleteBlogCategoryService
    {
        ResultDto Execute(long id);
    }
}
