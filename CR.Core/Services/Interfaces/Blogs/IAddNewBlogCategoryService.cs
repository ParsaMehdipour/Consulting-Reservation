using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IAddNewBlogCategoryService
    {
        ResultDto Execute(RequestAddNewBlogCategoryDto request);
    }
}
