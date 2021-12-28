using System.ComponentModel;
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

    public enum BloodType
    {
        [Display(Name = "O+")]
        OPositive,
        [Display(Name = "A+")]
        APositive,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "AB+")]
        ABPositive,
        [Display(Name = "AB-")]
        ABNegative,
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "O-")]
        ONegative

    }

    public enum AppointmentStatus
    {
        [Display(Name = "لغو شده")]
        Denied = 0,
        [Display(Name = "معلق")]
        Waiting = 0,
        [Display(Name = "تایید شده")]
        Accepted = 0,
        [Display(Name = "تکمیل شده")]
        Completed = 0,
    }
}

