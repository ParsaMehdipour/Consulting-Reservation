using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddDeclineFinancialTransactionService
    {
        ResultDto Execute(long payerId, int price, long factorId);
    }
}
