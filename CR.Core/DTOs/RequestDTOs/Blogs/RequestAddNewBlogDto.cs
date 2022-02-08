using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.RequestDTOs.Blogs
{
    public class RequestAddNewBlogDto
    {
        [Required(ErrorMessage = "عنوان مقاله را وارد کنید")]
        public string title { get; set; }

        public string slug { get; set; }

        [Required(ErrorMessage = "گروه مقاله را وارد کنید")]
        public long blogCategoryId { get; set; }

        [Required(ErrorMessage = "توضیحات کوتاه را وارد کنید")]
        public string shortDescription { get; set; }

        [Required(ErrorMessage = "توضیحات را وارد کنید")]
        public string description { get; set; }

        [Required(ErrorMessage = "ترتیب نمایش را وارد کنید")]
        public int orderNumber { get; set; }

        public string keyWords { get; set; }

        public bool status { get; set; }

        [Required(ErrorMessage = "توضیحات متا را وارد کنید")]
        public string metaDescription { get; set; }

        public string canonicalAddress { get; set; }

        [Required(ErrorMessage = "تاریخ انتشار را وارد کنید")]
        public string publishDate { get; set; }

        public IFormFile file { get; set; }
    }
}
