namespace CR.Core.DTOs.Payment
{
    public class RedirectToPaymentForWalletChargeDto
    {
        public int transactionNumber { get; set; }
        public long price { get; set; }
    }
}
