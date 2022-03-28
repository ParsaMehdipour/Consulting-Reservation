namespace CR.Core.DTOs.FinancialTransactions
{
    public class CheckoutFinancialTransactionForAdminDto
    {
        public long ReceiverId { get; set; }
        public string ReceiverIconSrc { get; set; }
        public string ReceiverFullName { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}
