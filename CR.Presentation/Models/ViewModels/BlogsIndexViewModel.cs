using CR.Core.DTOs.BlogCategories;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.ResultDTOs.Blogs;
using System.Collections.Generic;

namespace CR.Presentation.Models.ViewModels
{
    public class BlogsIndexViewModel
    {
        public ResultGetBlogsForPresentationDto ResultGetBlogsForPresentationDto { get; set; }
        public List<BlogForPresentationDto> LatestBlogsDto { get; set; }
        public List<BlogCategoryForSideBarDto> BlogCategoryForSideBarDtos { get; set; }
        public List<string> KeyWords { get; set; }
    }
}
