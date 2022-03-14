using CR.Common.DTOs;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogsKeywordsForPresentationService
    {
        ResultDto<List<string>> Execute();
    }
}
