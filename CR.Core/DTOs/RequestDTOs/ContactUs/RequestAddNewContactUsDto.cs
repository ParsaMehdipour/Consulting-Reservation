using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.RequestDTOs.ContactUs
{
    public class RequestAddNewContactUsDto
    {
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی خود را وارد کنید")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "لطفا پست الکترونیکی خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "آدرس پست الکترونیکی وارد شده معتبر نیست")]
        public string email { get; set; }

        [Required(ErrorMessage = "لطفا پیام خود را وارد کنید")]
        public string message { get; set; }
    }
}
