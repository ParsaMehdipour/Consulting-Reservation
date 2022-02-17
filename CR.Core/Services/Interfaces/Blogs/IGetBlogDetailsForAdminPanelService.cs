using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogDetailsForAdminPanelService
    {
        ResultDto<BlogDetailsForAdminDto> Execute(long id);
    }
}
