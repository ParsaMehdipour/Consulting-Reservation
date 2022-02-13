using CR.Common.DTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IUpdateFinancialTransactionStatusService
    {
        ResultDto Execute(string transactionNumber, TransactionStatus status);
    }
}
