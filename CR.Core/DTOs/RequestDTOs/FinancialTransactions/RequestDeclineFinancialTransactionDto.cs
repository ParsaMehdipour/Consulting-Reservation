namespace CR.Core.DTOs.RequestDTOs.FinancialTransactions
{
    public class RequestDeclineFinancialTransactionDto
    {
        public long factorId { get; set; }
        public long payerId { get; set; }
        public int price { get; set; }
    }
}
