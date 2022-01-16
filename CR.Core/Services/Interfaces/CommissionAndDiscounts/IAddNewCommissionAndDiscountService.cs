using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.CommissionAndDiscounts
{
    public interface IAddNewCommissionAndDiscountService
    {
        ResultDto Execute(RequestAddNewCommissionAndDiscountDto request);
    }
}
