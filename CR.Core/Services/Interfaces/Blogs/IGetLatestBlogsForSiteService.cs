using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetLatestBlogsForSiteService
    {
        ResultDto<List<BlogForPresentationDto>> Execute();
    }
}
