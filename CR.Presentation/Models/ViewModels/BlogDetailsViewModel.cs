using CR.Core.DTOs.BlogCategories;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;
using CR.Core.DTOs.Comments.Blogs;

namespace CR.Presentation.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public BlogDetailsForPresentationDto BlogDetailsForPresentationDto { get; set; }
        public List<BlogForPresentationDto> LatestBlogsDto { get; set; }
        public List<BlogCategoryForSideBarDto> BlogCategoryForSideBarDtos { get; set; }
        public List<BlogCommentDto> BlogCommentsDto { get; set; }
    }
}
