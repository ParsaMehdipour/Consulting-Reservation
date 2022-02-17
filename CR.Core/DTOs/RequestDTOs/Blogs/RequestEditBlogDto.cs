using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Blogs
{
    public class RequestEditBlogDto
    {
        public long id { get; set; }
        public string title { get; set; }

        public string slug { get; set; }

        public long blogCategoryId { get; set; }

        public string shortDescription { get; set; }

        public string description { get; set; }

        public int orderNumber { get; set; }

        public string keyWords { get; set; }

        public bool status { get; set; }

        public string metaDescription { get; set; }

        public string canonicalAddress { get; set; }

        public string publishDate { get; set; }

        public IFormFile file { get; set; }
    }
}
