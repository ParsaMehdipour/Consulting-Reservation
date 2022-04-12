using CR.DataAccess.Enums;

namespace CR.Core.DTOs.FinancialTransactions
{
    public class FinancialTransactionForAdminDto
    {
        public long PayerId { get; set; }
        public string PayerIconSrc { get; set; }
        public string PayerFullName { get; set; }
        public long ReceiverId { get; set; }
        public string ReceiverIconSrc { get; set; }
        public string ReceiverFullName { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public UserFlag PayerUserFlag { get; set; }
        public UserFlag ReceiverUserFlag { get; set; }
    }
}
