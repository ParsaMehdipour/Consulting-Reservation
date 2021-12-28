using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace CR.Core.DTOs.Users
{
    public class RegisterExpertFromAdminDto
    {
        [Required(ErrorMessage = "نام متخصص را وارد کنید")]
        [Display(Name = "نام متخصص")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی متخصص را وارد کنید")]
        [Display(Name = "نام خانوادگی متخصص")]
        public string lastName { get; set; }

        public int gender { get; set; }

        [Required(ErrorMessage = "شماره تماس متخصص را وارد کنید")]
        [Display(Name = "شماره تماس متخصص")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی متخصص را وارد کنید")]
        [Display(Name = "پست الکترونیکی متخصص")]
        public string email { get; set; }

        [Required(ErrorMessage = "رمزعبور را وارد کنید")]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور را وارد کنید")]
        [Display(Name = "تکرار رمزعبور")]
        [Compare(nameof(password), ErrorMessage = "رمزعبور با تکرار آن مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Required(ErrorMessage = "استان متخصص را وارد کنید")]
        [Display(Name = "استان متخصص")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر متخصص را وارد کنید")]
        [Display(Name = "شهر متخصص")]
        public string city { get; set; }

        [Required(ErrorMessage = "آدرس متخصص را وارد کنید")]
        [Display(Name = "آدرس متخصص")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کد پستی متخصص را وارد کنید")]
        [Display(Name = "کد پستی متخصص")]
        public string postalCode { get; set; }
    }
}
