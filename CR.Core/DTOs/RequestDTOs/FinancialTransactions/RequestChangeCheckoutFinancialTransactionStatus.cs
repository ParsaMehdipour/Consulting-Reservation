using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.FinancialTransactions
{
    public class RequestChangeCheckoutFinancialTransactionStatus
    {
        public long transactionId { get; set; }
        public TransactionStatus transactionStatus { get; set; }
    }
}
