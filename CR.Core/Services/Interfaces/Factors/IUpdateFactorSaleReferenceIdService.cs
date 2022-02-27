using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorSaleReferenceIdService
    {
        ResultDto Execute(long factorId, long saleReferenceId);
    }
}
