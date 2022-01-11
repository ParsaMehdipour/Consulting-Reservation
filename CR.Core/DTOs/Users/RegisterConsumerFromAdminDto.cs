using System.ComponentModel.DataAnnotations;
using CR.DataAccess.Enums;

namespace CR.Core.DTOs.Users
{
    public class RegisterConsumerFromAdminDto
    {
        [Required(ErrorMessage = "نام مراجعه کننده را وارد کنید")]
        [Display(Name = "نام مراجعه کننده")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی مراجعه کننده را وارد کنید")]
        [Display(Name = "نام خانوادگی مراجعه کننده")]
        public string lastName { get; set; }

        public GenderType gender { get; set; }

        [Required(ErrorMessage = "شماره تماس مراجعه کننده را وارد کنید")]
        [Display(Name = "شماره تماس مراجعه کننده")]
        [Phone]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی مراجعه کننده را وارد کنید")]
        [Display(Name = "پست الکترونیکی مراجعه کننده")]
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

        [Required(ErrorMessage = "استان مراجعه کننده را وارد کنید")]
        [Display(Name = "استان مراجعه کننده")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر مراجعه کننده را وارد کنید")]
        [Display(Name = "شهر مراجعه کننده")]
        public string city { get; set; }

        [Required(ErrorMessage = "آدرس مراجعه کننده را وارد کنید")]
        [Display(Name = "آدرس مراجعه کننده")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کد پستی مراجعه کننده را وارد کنید")]
        [Display(Name = "کد پستی مراجعه کننده")]
        public string postalCode { get; set; }
    }
}
