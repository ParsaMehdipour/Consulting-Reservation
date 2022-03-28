using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddCheckoutFinancialTransactionService
    {
        ResultDto Execute(long receiverId, int price);
    }
}
