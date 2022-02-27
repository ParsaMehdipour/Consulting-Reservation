using CR.Common.DTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorStatusService
    {
        ResultDto Execute(long factorId, FactorStatus factorStatus, TransactionStatus transactionStatus);
    }
}
