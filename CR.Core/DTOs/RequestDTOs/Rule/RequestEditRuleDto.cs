namespace CR.Core.DTOs.RequestDTOs.Rule
{
    public class RequestEditRuleDto
    {
        public long id { get; set; }
        public string fullContent { get; set; }
        public string paymentContent { get; set; }
        public string commentContent { get; set; }
        public string otherContent { get; set; }
    }
}
