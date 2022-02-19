using System;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Consumers
{
    public class ConsumerDetailsForAdminDto
    {
        public long consumerInformationId { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام مراجعه کننده را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی مراجعه کننده را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "استان مراجعه کننده را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر مراجعه کننده را وارد کنید")]
        public string city { get; set; }

        public DateTime birthDate { get; set; }

        public string birthDate_String { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی مراجعه کننده را وارد کنید")]
        [EmailAddress(ErrorMessage = "پست الکترونیکی معتبر نیست")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس مراجعه کننده را وارد کنید")]
        [Phone(ErrorMessage = "شماره تماس مورد تایید نیست")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس مراجعه کننده را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "میزان تحصیلات مراجعه کننده را وارد کنید")]
        public string degree { get; set; }
    }
}
