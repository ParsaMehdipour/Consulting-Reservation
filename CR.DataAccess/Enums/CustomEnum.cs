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

    public enum FactorStatus
    {
        [Display(Name = "ثبت اولیه")]
        Waiting = 0,
        [Display(Name = "پرداخت موفق")]
        SuccessfulPayment = 1,
        [Display(Name = "پرداخت ناموفق")]
        UnsuccessfulPayment = 2,
    }

    public enum TimingType
    {
        [Display(Name = "کوتاه مدت")]
        ShortSpan = 0,
        [Display(Name = "میان مدت")]
        MediumSpan = 1,
        [Display(Name = "بلند مدت")]
        LongSpan = 2,
    }

    public enum CallingType
    {
        [Display(Name = "تلفنی")]
        PhoneCall = 0,
        [Display(Name = "صوتی")]
        VoiceCall = 1,
        [Display(Name = "متنی")]
        TextCall = 2,
    }

    public enum TransactionStatus
    {
        [Display(Name = "نامشخص")]
        UnDefined = 0,
        [Display(Name = "ناموفق")]
        Failed = 1,
        [Display(Name = "موفق")]
        Successful = 2,

    }

    public enum TransactionType
    {
        [Display(Name = "پرداخت از کارت بانکی")]
        PayFromCreditCard = 0,
        [Display(Name = "پرداخت از کیف پول")]
        PayFromWallet = 1,
        [Display(Name = "شارژ کیف پول")]
        ChargeWallet = 2,
    }
}

