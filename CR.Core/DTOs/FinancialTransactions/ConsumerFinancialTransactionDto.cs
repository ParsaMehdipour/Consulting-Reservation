﻿namespace CR.Core.DTOs.FinancialTransactions
{
    public class ConsumerFinancialTransactionDto
    {
        public long ReceiverId { get; set; }
        public string ReceiverIconSrc { get; set; }
        public string ReceiverFullName { get; set; }
        public long PayerId { get; set; }
        public string payerIconSrc { get; set; }
        public string PayerFullName { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
    }
}
