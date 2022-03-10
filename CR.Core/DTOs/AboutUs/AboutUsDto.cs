using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.AboutUs
{
    public class AboutUsDto
    {
        public long Id { get; set; }

        [Required]
        public string FullContent { get; set; }
    }
}
