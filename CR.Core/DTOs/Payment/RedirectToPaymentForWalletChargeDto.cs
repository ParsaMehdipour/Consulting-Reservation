namespace CR.Core.DTOs.Payment
{
    public class RedirectToPaymentForWalletChargeDto
    {
        public string transactionNumber { get; set; }
        public long price { get; set; }
    }
}
