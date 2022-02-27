namespace CR.Core.DTOs.Payment
{
    public class RedirectToPaymentForReservationDto
    {
        public string transactionNumber { get; set; }
        public int price { get; set; }
    }
}
