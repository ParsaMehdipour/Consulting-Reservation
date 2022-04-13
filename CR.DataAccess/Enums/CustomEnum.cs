using System.ComponentModel.DataAnnotations;

namespace CR.DataAccess.Enums
{
    public enum UserFlag
    {
        [Display(Name = "کاربر")]
        Consumer = 0,
        [Display(Name = "مشاور")]
        Expert = 1,
        [Display(Name = "مدیریت")]
        Admin = 2,
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
        [Display(Name = "لغو شده")]
        Declined = 3,
        [Display(Name = "ثبت اولیه")]
        Temporary = 4

    }

    public enum FactorStatus
    {
        [Display(Name = "در انتظار پرداخت")]
        Waiting = 0,
        [Display(Name = "پرداخت موفق")]
        SuccessfulPayment = 1,
        [Display(Name = "پرداخت ناموفق")]
        UnsuccessfulPayment = 2,
        [Display(Name = "مرجوع")]
        Declined = 3,
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
        [Display(Name = "مشاوره تلفنی")]
        PhoneCall = 0,
        [Display(Name = "مشاوره صوتی")]
        VoiceCall = 1,
        [Display(Name = "مشاوره متنی")]
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
        [Display(Name = "پرداخت از کارت بانکی کاربر")]
        PayFromCreditCard = 0,
        [Display(Name = "پرداخت از کیف پول کاربر")]
        PayFromWallet = 1,
        [Display(Name = "شارژ کیف پول کاربر")]
        ChargeWallet = 2,
        [Display(Name = "شارژ کیف پول کاربر - لغو نوبت")]
        DeclineAppointmentTransaction = 3,
        [Display(Name = "شارژ کیف پول کاربر - لغو فاکتور")]
        DeclineFactorTransaction = 4,
        [Display(Name = "شارژ کیف پول مشاور")]
        ChargeExpertWallet = 5,
        [Display(Name = "تسویه حساب مشاور")]
        Checkout = 6,
        [Display(Name = "پرداخت جهت شارژ کیف پول کاربر")]
        PayForWalletCharge = 7
    }

    public enum CommentStatus
    {
        [Display(Name = "در انتظار")]
        Waiting = 0,
        [Display(Name = "تایید شده")]
        Accepted = 1,
        [Display(Name = "کنسل شده")]
        Declined = 2,
    }

    public enum CommentType
    {
        [Display(Name = "متخصص")]
        Expert = 0,
        [Display(Name = "مقاله")]
        Blog = 1
    }

    public enum MessageFlag
    {
        [Display(Name = "پیام کاربر")]
        ConsumerMessage = 0,
        [Display(Name = "پیام متخصص")]
        ExpertMessage = 1
    }

    public enum ImageType
    {
        [Display(Name = "رزومه")]
        Resume = 0,
        [Display(Name = "مدرک")]
        Degree = 1,
        [Display(Name = "سایر")]
        Other = 2,
    }
    public enum RuleType
    {
        Full = 0,
        Payment = 1,
        Comment = 2,
        Other = 3,
    }

    public enum MessageType
    {
        [Display(Name = "مشاوره متنی")]
        Text = 0,
        [Display(Name = "مشاوره صوتی")]
        Voice = 1
    }
}

