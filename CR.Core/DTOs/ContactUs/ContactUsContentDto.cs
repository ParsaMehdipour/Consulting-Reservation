using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.ContactUs
{
    public class ContactUsContentDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "لطفا متن تماس با ما را وارد کنید")]
        public string FullContent { get; set; }
    }
}
