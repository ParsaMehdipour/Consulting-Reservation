using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.CommissionAndDiscounts
{
    public interface IGetAllCommissionAndDiscountsForAdminService
    {
        ResultDto<ResultGetALLCommissionAndDiscountsForAdminDto> Execute();
    }
}
