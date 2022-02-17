using System;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Blogs
{
    public class BlogDetailsForAdminDto
    {

        public long id { get; set; }

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

        public DateTime publishDate { get; set; }

        public string pictureSrc { get; set; }
    }
}
