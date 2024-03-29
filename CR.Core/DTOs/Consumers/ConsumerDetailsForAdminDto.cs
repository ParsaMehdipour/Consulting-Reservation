﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Consumers
{
    public class ConsumerDetailsForAdminDto
    {
        public long consumerInformationId { get; set; }

        public string iconSrc { get; set; }

        [Required(ErrorMessage = "نام کاربر را وارد کنید")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی کاربر را وارد کنید")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "استان کاربر را وارد کنید")]
        public string province { get; set; }

        [Required(ErrorMessage = "شهر کاربر را وارد کنید")]
        public string city { get; set; }

        public DateTime birthDate { get; set; }

        public string birthDate_String { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی کاربر را وارد کنید")]
        [EmailAddress(ErrorMessage = "پست الکترونیکی معتبر نیست")]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره تماس کاربر را وارد کنید")]
        [Phone(ErrorMessage = "شماره تماس مورد تایید نیست")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "آدرس کاربر را وارد کنید")]
        public string specificAddress { get; set; }

        [Required(ErrorMessage = "میزان تحصیلات کاربر را وارد کنید")]
        public string degree { get; set; }
    }
}
