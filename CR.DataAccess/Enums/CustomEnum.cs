﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CR.DataAccess.Enums
{
    public enum UserFlag
    {
        [Display(Name = "مراجعه کننده")]
        Consumer = 0,
        [Display(Name = "پزشک")]
        Expert = 1,
    }

    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2,
    }

    public enum AppointmentStatus
    {
        [Display(Name = "در انتظار")]
        Waiting = 0,
        [Display(Name = "انجام نشد")]
        NotDone = 1,
        [Display(Name = "انجام شد")]
        Completed = 2,
    }
}

