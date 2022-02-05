using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.CommissionAndDiscounts;

namespace CR.Core.Services.Interfaces.CommissionAndDiscounts
{
    public interface IGetAllCommissionAndDiscountsForAdminService
    {
        ResultDto<ResultGetALLCommissionAndDiscountsForAdminDto> Execute();
    }
}
