using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Factors;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorStatusService
    {
        ResultDto<ResultUpdateFactorStatusDto> Execute(long factorId, FactorStatus factorStatus, TransactionStatus transactionStatus);
    }
}
