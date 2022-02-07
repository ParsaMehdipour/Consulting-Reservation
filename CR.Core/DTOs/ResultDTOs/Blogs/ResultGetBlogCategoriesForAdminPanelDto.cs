﻿using CR.Core.DTOs.Blogs;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Blogs
{
    public class ResultGetBlogCategoriesForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<BlogCategoryForAdminPanelDto> BlogCategoryForAdminPanelDtos { get; set; }
    }
}