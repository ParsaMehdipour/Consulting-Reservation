using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Account
{
    public class RegisterExpertViewModel
    {
        [Required(ErrorMessage = "کد تایید را وارد کنید")]
        [Display(Name = "کد تایید")]
        public string verificationCode { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [Display(Name = "نام مشاور")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        [Display(Name = "نام خانوادگی مشاور")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        [Display(Name = "شماره تماس")]
        [Phone]
        [RegularExpression(@"(\+98|0)?9\d{9}",
         ErrorMessage = "شماره موبایل را به درستی وارد کنید")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "رمزعبور را وارد کنید")]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور را وارد کنید")]
        [Display(Name = "تکرار رمزعبور")]
        [Compare(nameof(Password), ErrorMessage = "رمزعبور با تکرار آن مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
