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

        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد کنید")]
        [Phone(ErrorMessage = "شماره تماس وارد شده معتبر نیست")]
        [StringLength(11, ErrorMessage = "طول شماره تماس وارد شده درست نیست", MinimumLength = 11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس وارد شده معتبر نیست")]
        public string phone { get; set; }

        [Required(ErrorMessage = "لطفا پیام خود را وارد کنید")]
        public string message { get; set; }
    }
}
