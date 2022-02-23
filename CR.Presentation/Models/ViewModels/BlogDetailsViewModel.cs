using CR.Core.DTOs.Blogs;
using System.Collections.Generic;

namespace CR.Presentation.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public BlogDetailsForPresentationDto BlogDetailsForPresentationDto { get; set; }
        public List<BlogForPresentationDto> LatestBlogsDto { get; set; }
    }
}
