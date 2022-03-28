using CR.Common.DTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IChangeCheckoutFinancialTransactionService
    {
        ResultDto Execute(long transactionId, TransactionStatus status);
    }
}
