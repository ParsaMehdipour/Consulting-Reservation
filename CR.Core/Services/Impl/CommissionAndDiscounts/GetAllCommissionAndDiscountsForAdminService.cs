using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.CommissionAndDiscounts;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.CommissionAndDiscounts;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.CommissionAndDiscounts;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.CommissionAndDiscounts
{
    public class GetAllCommissionAndDiscountsForAdminService : IGetAllCommissionAndDiscountsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetAllCommissionAndDiscountsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetALLCommissionAndDiscountsForAdminDto> Execute()
        {
            var commissionAndDiscounts = _context.ExpertInformations
                .Include(e=>e.Specialty)
                .Include(e => e.CommissionAndDiscount)
                .OrderByDescending(e => e.Id)
                .Select(e=> new CommissionAndDiscountForAdminDto
                {
                    expertFullName = e.FirstName + " " + e.LastName,
                    expertIconSrc = e.IconSrc ?? "assets/img/icon-256x256.png",
                    expertId = e.ExpertId,
                    expertInformationId = e.Id,
                    expertSpeciality = (e.Specialty != null) ? e.Specialty.Name : "تخصص نامعلوم",
                    hasValue = e.CommissionAndDiscount != null,
                    id = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.Id : 0,
                    phoneCallCommission = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.PhoneCallCommission.ToString(CultureInfo.InvariantCulture) : "0",
                    voiceCallCommission = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.VoiceCallCommission.ToString(CultureInfo.InvariantCulture) : "0",
                    textCallCommission = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.TextCallCommission.ToString(CultureInfo.InvariantCulture) : "0",
                    phoneCallDiscount = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.PhoneCallDiscount.ToString(CultureInfo.InvariantCulture) : "0",
                    voiceCallDiscount = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.VoiceCallDiscount.ToString(CultureInfo.InvariantCulture) : "0",
                    textCallDiscount = (e.CommissionAndDiscount != null) ? e.CommissionAndDiscount.TextCallDiscount.ToString(CultureInfo.InvariantCulture) : "0",
                }).ToList();

            return new ResultDto<ResultGetALLCommissionAndDiscountsForAdminDto>()
            {
                Data = new ResultGetALLCommissionAndDiscountsForAdminDto()
                {
                    CommissionAndDiscountForAdminDtos = commissionAndDiscounts
                },
                IsSuccess = true
            };
        }
    }
}
