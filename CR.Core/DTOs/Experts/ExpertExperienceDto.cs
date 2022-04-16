namespace CR.Core.DTOs.Experts
{
    public class ExpertExperienceDto
    {
        public string clinicName { get; set; }

        //[Range(0, 9999, ErrorMessage = "لطفا سال شروع را درست وارد کنید")]
        public string startYear { get; set; }

        //[Range(0, 9999, ErrorMessage = "لطفا سال پایان را درست وارد کنید")]
        public string finishYear { get; set; }

        public string role { get; set; }
    }
}
