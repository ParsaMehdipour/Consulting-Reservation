﻿using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;
using CR.Core.DTOs.Comments.Blogs;

namespace CR.Presentation.Areas.AdminPanel.Models.ViewModels
{
    public class AdminBlogDetailsViewModel
    {
        public BlogDetailsDto BlogDetailsDto { get; set; }
        public List<BlogCommentDto> BlogCommentDtos { get; set; }
    }
}
