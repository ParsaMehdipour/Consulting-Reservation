using System.Globalization;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.CommissionAndDiscounts;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.CommissionAndDiscounts
{
    public class GetCommissionAndDiscountDetailsForAdminService : IGetCommissionAndDiscountDetailsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetCommissionAndDiscountDetailsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<CommissionAndDiscountDetailsForAdminDto> Execute(long id)
        {
            var commissionAndDiscount = _context.CommissionAndDiscounts.FirstOrDefault(e => e.Id == id);

            if (commissionAndDiscount == null)
            {
                return new ResultDto<CommissionAndDiscountDetailsForAdminDto>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "اطلاعات کارمزد و تخفیف یافت نشد!!"
                };
            }

            return new ResultDto<CommissionAndDiscountDetailsForAdminDto>()
            {
                Data = new CommissionAndDiscountDetailsForAdminDto()
                {
                    id = commissionAndDiscount.Id,
                    phoneCallCommission = commissionAndDiscount.PhoneCallCommission.ToString(CultureInfo.InvariantCulture),
                    voiceCallCommission = commissionAndDiscount.VoiceCallCommission.ToString(CultureInfo.InvariantCulture),
                    textCallCommission = commissionAndDiscount.TextCallCommission.ToString(CultureInfo.InvariantCulture),
                    phoneCallDiscount = commissionAndDiscount.PhoneCallDiscount.ToString(CultureInfo.InvariantCulture),
                    voiceCallDiscount = commissionAndDiscount.VoiceCallDiscount.ToString(CultureInfo.InvariantCulture),
                    textCallDiscount = commissionAndDiscount.TextCallDiscount.ToString(CultureInfo.InvariantCulture)
                },
                IsSuccess = true
            };
        }
    }
}
