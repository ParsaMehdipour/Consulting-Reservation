using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IDeleteBlogService
    {
        ResultDto Execute(long id);
    }
}
