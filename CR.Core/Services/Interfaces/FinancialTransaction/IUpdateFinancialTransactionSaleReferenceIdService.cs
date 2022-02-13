using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IUpdateFinancialTransactionSaleReferenceIdService
    {
        ResultDto Execute(string transactionNumber, long saleReferenceId);
    }
}
