namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewCommissionAndDiscountDto
    {
        public long expertInformationId { get; set; }
        public string phoneCallCommission { get; set; } = "0";
        public string voiceCallCommission { get; set; } = "0";
        public string textCallCommission { get; set; } = "0";
        public string phoneCallDiscount { get; set; } = "0";
        public string voiceCallDiscount { get; set; } = "0";
        public string textCallDiscount { get; set; } = "0";
    }
}
