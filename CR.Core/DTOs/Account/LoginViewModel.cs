using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "شماره موبایل را وارد کنید")]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمزعبور خود را وارد کنید")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

    }
}
