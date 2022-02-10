using CR.Common.DTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorStatusService
    {
        ResultDto Execute(string factorNumber, FactorStatus factorStatus, TransactionStatus transactionStatus);
    }
}
