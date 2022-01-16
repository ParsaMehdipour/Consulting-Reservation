namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditCommissionAndDiscountDto
    {
        public long id { get; set; }
        public string phoneCallCommission { get; set; }
        public string voiceCallCommission { get; set; }
        public string textCallCommission { get; set; }
        public string phoneCallDiscount { get; set; }
        public string voiceCallDiscount { get; set; }
        public string textCallDiscount { get; set; }
    }
}
