using CR.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Users
{
    public class RegisterConsumerFromAdminDto
    {
        [Required(ErrorMessage = "نام کاربر را وارد کنید")]
        [Display(Name = "نام کاربر")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی کاربر را وارد کنید")]
        [Display(Name = "نام خانوادگی کاربر")]
        public string lastName { get; set; }

        public GenderType gender { get; set; }

        [Required(ErrorMessage = "شماره تماس کاربر را وارد کنید")]
        [Display(Name = "شماره تماس کاربر")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی کاربر را وارد کنید")]
        [Display(Name = "پست الکترونیکی کاربر")]
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

        [Required(ErrorMessage = "استان کاربر را وارد کنید")]
        [Display(Name = "استان کاربر")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر کاربر را وارد کنید")]
        [Display(Name = "شهر کاربر")]
        public string city { get; set; }

        [Required(ErrorMessage = "آدرس کاربر را وارد کنید")]
        [Display(Name = "آدرس کاربر")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "میزان تحصیلات کاربر را وارد کنید")]
        [Display(Name = "میزان تحصیلات کاربر")]
        public string degree { get; set; }
    }
}
