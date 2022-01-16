using System.Collections.Generic;
using CR.Core.DTOs.CommissionAndDiscounts;
using CR.DataAccess.Entities.CommissionAndDiscounts;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetALLCommissionAndDiscountsForAdminDto
    {
        public List<CommissionAndDiscountForAdminDto> CommissionAndDiscountForAdminDtos { get; set; }
    }
}
