using CR.Core.DTOs.FinancialTransactions;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.FinancialTransactions
{
    public class ResultGetCheckoutFinancialTransactionsDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<CheckoutFinancialTransactionForAdminDto> CheckoutFinancialTransactionForAdminDtos { get; set; }
    }
}
