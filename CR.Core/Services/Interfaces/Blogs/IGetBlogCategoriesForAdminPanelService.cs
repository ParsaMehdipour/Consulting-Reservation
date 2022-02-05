﻿using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Blogs;

namespace CR.Core.Services.Interfaces.Blogs
{
    public interface IGetBlogCategoriesForAdminPanelService
    {
        ResultDto<ResultGetBlogCategoriesForAdminPanelDto> Execute(long? parentId, int Page = 1, int PageSize = 20);
    }
}
