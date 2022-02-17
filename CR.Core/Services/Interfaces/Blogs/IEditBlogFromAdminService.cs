using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IEditBlogFromAdminService
    {
        ResultDto Execute(RequestEditBlogDto request);
    }
}
