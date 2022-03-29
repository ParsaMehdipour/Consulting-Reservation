using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;
using CR.Core.DTOs.Comments.Blogs;

namespace CR.Presentation.Areas.ExpertPanel.Models.ViewModels
{
    public class ExpertBlogDetailsViewModel
    {
        public List<BlogCommentDto> BlogCommentDtos { get; set; }
        public BlogDetailsDto BlogDetailsDto { get; set; }
    }
}
