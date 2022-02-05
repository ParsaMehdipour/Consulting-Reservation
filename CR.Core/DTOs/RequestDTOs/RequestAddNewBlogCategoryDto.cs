using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewBlogCategoryDto
    {
        public long? parentId { get; set; }

        [Required(ErrorMessage = "عنوان گروه مقاله را وارد کنید")]
        public string name { get; set; }

        [Required(ErrorMessage = "Slug را وارد کنید")]
        public string slug { get; set; }

        [Required(ErrorMessage = "ترتیب نمایش را وارد کنید")]
        public int showOrder { get; set; }

        public string description { get; set; }

        public string metaDescription { get; set; }

        public IFormFile file { get; set; }
    }
}
