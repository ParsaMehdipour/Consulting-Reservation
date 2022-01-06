namespace CR.Core.DTOs.Payment
{
    public class RequestPaymentDto
    {
        public long terminalId { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public long orderId { get; set; }
        public long amount { get; set; }
        public string localDate { get; set; }
        public string localTime { get; set; }
        public string additionalData { get; set; }
        public string callBackUrl { get; set; }
        public long payerId { get; set; }
    }
}
