using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForAdminDto
    {
        public long expertInformationId { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام متخصص را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی متخصص را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "استان متخصص را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر متخصص را وارد کنید")]
        public string city { get; set; }


        public string bio { get; set; }

        [Required(ErrorMessage = "تاریخ تولد متخصص را وارد کنید")]
        public string birthDate { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی متخصص را وارد کنید")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس متخصص را وارد کنید")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس متخصص را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کدپستی متخصص را وارد کنید")]
        public string postalCode { get; set; }
    }
}
