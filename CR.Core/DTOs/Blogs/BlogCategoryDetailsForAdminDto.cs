using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Blogs
{
    public class BlogCategoryDetailsForAdminDto
    {
        public long id { get; set; }

        public string pictureSrc { get; set; }

        [Required(ErrorMessage = "Slug را وارد کنید")]
        public string slug { get; set; }

        [Required(ErrorMessage = "عنوان گروه مقاله را وارد کنید")]
        public string name { get; set; }

        [Required(ErrorMessage = "ترتیب نمایش را وارد کنید")]
        public int orderNumber { get; set; }

        public string description { get; set; }

        public string metaDescription { get; set; }
    }
}
