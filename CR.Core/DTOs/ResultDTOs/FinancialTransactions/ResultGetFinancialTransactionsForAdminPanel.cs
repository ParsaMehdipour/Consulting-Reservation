using CR.Core.DTOs.FinancialTransactions;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.FinancialTransactions
{
    public class ResultGetFinancialTransactionsForAdminPanel
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<FinancialTransactionForAdminDto> FinancialTransactionForAdminDtos { get; set; }
    }
}
