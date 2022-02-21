using System;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForAdminDto
    {
        public long expertInformationId { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام مشاور را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی مشاور را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "استان مشاور را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر مشاور را وارد کنید")]
        public string city { get; set; }


        public string bio { get; set; }

        [Required(ErrorMessage = "تاریخ تولد مشاور را وارد کنید")]
        public DateTime birthDate { get; set; }
        public string birthDate_String { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی مشاور را وارد کنید")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس مشاور را وارد کنید")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس مشاور را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کدپستی مشاور را وارد کنید")]
        public string postalCode { get; set; }
    }
}
