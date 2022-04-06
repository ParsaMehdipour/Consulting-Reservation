using System.ComponentModel.DataAnnotations;


namespace CR.Core.DTOs.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        [Display(Name = "شماره تماس")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "کد تایید را وارد کنید")]
        [Display(Name = "کد تایید")]
        public string verificationCode { get; set; }

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
