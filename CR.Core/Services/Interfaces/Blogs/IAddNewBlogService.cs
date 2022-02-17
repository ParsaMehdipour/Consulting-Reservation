using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IAddNewBlogService
    {
        ResultDto Execute(RequestAddNewBlogDto request, long userId);
    }
}
