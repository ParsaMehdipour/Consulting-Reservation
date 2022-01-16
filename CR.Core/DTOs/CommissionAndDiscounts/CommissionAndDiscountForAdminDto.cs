namespace CR.Core.DTOs.CommissionAndDiscounts
{
    public class CommissionAndDiscountForAdminDto
    {
        public long id { get; set; }
        public long expertId { get; set; }
        public long expertInformationId { get; set; }
        public string expertIconSrc { get; set; }
        public string expertFullName { get; set; }
        public string expertSpeciality { get; set; }
        public bool hasValue { get; set; }
        public string phoneCallCommission { get; set; }
        public string voiceCallCommission { get; set; }
        public string textCallCommission { get; set; }
        public string phoneCallDiscount { get; set; }
        public string voiceCallDiscount { get; set; }
        public string textCallDiscount { get; set; }
    }
}
