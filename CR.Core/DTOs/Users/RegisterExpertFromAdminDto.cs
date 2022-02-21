using System.ComponentModel.DataAnnotations;
using CR.DataAccess.Enums;

// ReSharper disable All

namespace CR.Core.DTOs.Users
{
    public class RegisterExpertFromAdminDto
    {
        [Required(ErrorMessage = "نام مشاور را وارد کنید")]
        [Display(Name = "نام مشاور")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی مشاور را وارد کنید")]
        [Display(Name = "نام خانوادگی مشاور")]
        public string lastName { get; set; }

        public GenderType gender { get; set; }

        [Required(ErrorMessage = "شماره تماس مشاور را وارد کنید")]
        [Display(Name = "شماره تماس مشاور")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی مشاور را وارد کنید")]
        [Display(Name = "پست الکترونیکی مشاور")]
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

        [Required(ErrorMessage = "استان مشاور را وارد کنید")]
        [Display(Name = "استان مشاور")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر مشاور را وارد کنید")]
        [Display(Name = "شهر مشاور")]
        public string city { get; set; }

        [Required(ErrorMessage = "آدرس مشاور را وارد کنید")]
        [Display(Name = "آدرس مشاور")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کد پستی مشاور را وارد کنید")]
        [Display(Name = "کد پستی مشاور")]
        public string postalCode { get; set; }
    }
}
