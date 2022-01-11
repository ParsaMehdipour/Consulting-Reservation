using CR.DataAccess.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.Consumers
{
    public class ConsumerDetailsForSiteDto
    {
        public long id { get; set; }

        public string userName { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        public string lastName { get; set; }

        public IFormFile iconImage { get; set; }

        public string birthDate_String { get; set; }

        public DateTime birthDate { get; set; }

        public GenderType gender { get; set; }

        [Required(ErrorMessage = "گروه خونی را وارد کنید")]
        public string bloodType { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی را وارد کنید")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "استان را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر را وارد کنید")]
        public string city { get; set; }

        [Required(ErrorMessage = "کد پستی را وارد کنید")]
        public string postalCode { get; set; }

        public string iconSrc { get; set; }
    }
}
