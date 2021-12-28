using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Account
{
    public class ResetPasswordFromAdmin
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "رمزعبور جدید را وارد کنید")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور را وارد کنید")]
        [Compare(nameof(newPassword), ErrorMessage = "رمزعبور با تکرار آن مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string confirmNewPassword { get; set; }
    }
}
