using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Account
{
    public class ResetPasswordForMainSiteViewModel
    {
        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        [Display(Name = "شماره تماس")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "کد تایید را وارد کنید")]
        [Display(Name = "کد تایید")]
        public string verificationCode { get; set; }

        [Required(ErrorMessage = "رمزعبور جدید را وارد کنید")]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور جدید را وارد کنید")]
        [Display(Name = "تکرار رمزعبور")]
        [Compare(nameof(NewPassword), ErrorMessage = "رمزعبور جدید با تکرار آن مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
