﻿using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace CR.Core.DTOs.Experts
{
    public class ExpertDetailsForProfileDto
    {
        public long? id { get; set; }

        public string userName { get; set; }

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

        public DateTime? birthDate { get; set; }

        [Required(ErrorMessage = "بیوگرافی خود را وارد کنید")]
        [MaxLength(250, ErrorMessage = "متن بیوگرافی بیشتر از 250 کاراکتر نمیتواند باشد")]
        public string bio { get; set; }

        //[Required(ErrorMessage = "تصویر خود را وارد کنید")]
        public IFormFile iconImage { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام محل کار خود را وارد کنید")]
        public string clinicName { get; set; }

        [Required(ErrorMessage = "آدرس محل کار خود را وارد کنید")]
        public string clinicAddress { get; set; }

        public List<IFormFile> ClininImages { get; set; }

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

        //[Required(ErrorMessage = "خدمات خود را وارد کنید")]
        public string tag { get; set; }

        public string instagram { get; set; }

        [Required(ErrorMessage = "شماره شبا را وارد کنید را وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره شبا وارد شده معتبر نیست")]
        [MaxLength(24, ErrorMessage = "طول شماره شبا وارد شده معتبر نیست")]
        [MinLength(24, ErrorMessage = "طول شماره شبا وارد شده معتبر نیست")]
        public string shabaNumber { get; set; }

        public long? specialtyId { get; set; }


        public List<string> Tags { get; set; }
        public List<ExpertExperienceDto> experiences { get; set; }
        public List<ExpertMembershipDto> memberships { get; set; }
        public List<ExpertPrizeDto> prizes { get; set; }
        public List<ExpertStudyDto> studies { get; set; }
        public List<ExpertSubscriptionDto> subscriptions { get; set; }
        public List<ExpertImageDto> resumeImages { get; set; }
        public List<ExpertImageDto> degreeImages { get; set; }
        public List<ExpertImageDto> otherImages { get; set; }
        public List<ExpertImageDto> identityImages { get; set; }
    }
}
