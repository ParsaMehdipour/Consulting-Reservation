using CR.Core.DTOs.FinancialTransactions;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.FinancialTransactions
{
    public class ResultGetExpertFinancialTransactionsDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int WalletBalance { get; set; }
        public List<ExpertFinancialTransactionDto> ExpertFinancialTransactionDtos { get; set; }
    }
}
