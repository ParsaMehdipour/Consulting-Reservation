using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.FinancialTransaction
{
    public interface IAddDeclineFactorFinancialTransactionService
    {
        ResultDto Execute(long receiverId, long factorId);
    }
}
