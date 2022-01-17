using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditBasicExpertDetailsDto
    {
        public long id { get; set; }
        [Required(ErrorMessage = "پست الکترونیکی خود را وارد کنید")]
        public string email { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "شماره تماس خود را وارد کنید")]
        public string phoneNumber { get; set; }

        public GenderType gender { get; set; }

        [Required(ErrorMessage = "تاریخ تولد خود را وارد کنید")]
        public string birthDate_String { get; set; }

        [Required(ErrorMessage = "بیوگرافی خود را وارد کنید")]
        public string bio { get; set; }

        //[Required(ErrorMessage = "تصویر خود را وارد کنید")]
        public IFormFile iconImage { get; set; }

        [Required(ErrorMessage = "نام کلینیک خود را وارد کنید")]
        public string clinicName { get; set; }

        [Required(ErrorMessage = "آدرس کلینیک خود را وارد کنید")]
        public string clinicAddress { get; set; }

        [Required(ErrorMessage = "استان خود را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر خود را وارد کنید")]
        public string city { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "کد پستی خود را وارد کنید")]
        public string postalCode { get; set; }

        public bool usePhoneCall { get; set; }

        [Required(ErrorMessage = "هزینه تماس تلفنی را وارد کنید را وارد کنید")]
        public string phoneCallPrice { get; set; }

        public bool useVoiceCall { get; set; }

        [Required(ErrorMessage = "هزینه تماس صوتی را وارد کنید را وارد کنید")]
        public string voiceCallPrice { get; set; }

        public bool useTextCall { get; set; }

        [Required(ErrorMessage = "هزینه تماس متنی را وارد کنید را وارد کنید")]
        public string textCallPrice { get; set; }

        public string tag { get; set; }

        public string instagram { get; set; }

        public long? specialtyId { get; set; }
        [DisplayName("image")]
        public List<IFormFile> image { get; set; }
    }
}
