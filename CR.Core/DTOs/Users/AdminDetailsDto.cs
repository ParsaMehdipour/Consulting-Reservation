using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Users
{
    public class AdminDetailsDto
    {
        public long? adminId { get; set; }

        public long? informationId { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "استان خود را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر خود را وارد کنید")]
        public string city { get; set; }

        [Required(ErrorMessage = "تاریخ تولد خود را وارد کنید")]
        public string birthDate { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی خود را وارد کنید")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس خود را وارد کنید")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کدپستی خود را وارد کنید")]
        public string postalCode { get; set; }

        [Required(ErrorMessage = "رمزعبور جدید را وارد کنید")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور را وارد کنید")]
        [Compare(nameof(newPassword), ErrorMessage = "رمزعبور با تکرار آن مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string confirmNewPassword { get; set; }
    }
}
