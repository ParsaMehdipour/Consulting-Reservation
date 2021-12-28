﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

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

        public DateTime birthDate { get; set; }

        [Required(ErrorMessage = "بیوگرافی خود را وارد کنید")]
        public string bio { get; set; }

        //[Required(ErrorMessage = "تصویر خود را وارد کنید")]
        public IFormFile iconImage { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام کلینیک خود را وارد کنید")]
        public string clinicName { get; set; }

        [Required(ErrorMessage = "آدرس کلینیک خود را وارد کنید")]
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

        [Required(ErrorMessage = "هزینه را وارد کنید")]
        public string price { get; set; }

        public bool isFreeOfCharge { get; set; }

        //[Required(ErrorMessage = "خدمات خود را وارد کنید")]
        public string tag { get; set; }
        public string instagram { get; set; }
        public long specialtyId { get; set; }


        public List<string> Tags { get; set; }
        public List<ExpertImageDto> Images { get; set; }
        public List<ExpertExperienceDto> ExpertExperienceDtos { get; set; }
        public List<ExpertMembershipDto> ExpertMembershipDtos { get; set; }
        public List<ExpertPrizeDto> ExpertPrizeDtos { get; set; }
        public List<ExpertStudyDto> ExpertStudyDtos { get; set; }
        public List<ExpertSubscriptionDto> ExpertSubscriptionDtos { get; set; }
    }
}
