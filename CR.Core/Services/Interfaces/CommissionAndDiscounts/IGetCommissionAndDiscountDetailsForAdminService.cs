using CR.Common.DTOs;
using CR.Core.DTOs.CommissionAndDiscounts;

namespace CR.Core.Services.Interfaces.CommissionAndDiscounts
{
    public interface IGetCommissionAndDiscountDetailsForAdminService
    {
        ResultDto<CommissionAndDiscountDetailsForAdminDto> Execute(long id);
    }
}
