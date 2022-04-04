namespace CR.Core.DTOs.FinancialTransactions
{
    public class ExpertFinancialTransactionDto
    {
        public long Id { get; set; }
        public string AdminIconSrc { get; set; }
        public string AdminFullName { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
    }
}
