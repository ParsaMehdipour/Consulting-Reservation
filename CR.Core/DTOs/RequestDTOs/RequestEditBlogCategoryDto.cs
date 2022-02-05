using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditBlogCategoryDto
    {
        public long id { get; set; }
        public IFormFile file { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int orderNumber { get; set; }
        public string description { get; set; }
        public string metaDescription { get; set; }
    }
}
