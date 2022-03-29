namespace CR.Core.DTOs.Payment
{
    public class RedirectToPaymentForWalletChargeDto
    {
        public string transactionNumber { get; set; }
        public int price { get; set; }
        public string phoneNumber { get; set; }
    }
}
