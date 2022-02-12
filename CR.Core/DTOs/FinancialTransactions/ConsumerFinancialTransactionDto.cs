namespace CR.Core.DTOs.FinancialTransactions
{
    public class ConsumerFinancialTransactionDto
    {
        public long? ExpertInformationId { get; set; }
        public string ExpertIconSrc { get; set; }
        public string ExpertFullName { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
    }
}
