using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.CommissionAndDiscounts
{
    public class EditCommissionAndDiscountService : IEditCommissionAndDiscountService
    {
        private readonly ApplicationContext _context;

        public EditCommissionAndDiscountService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditCommissionAndDiscountDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var commissionAndDiscount = _context.CommissionAndDiscounts.FirstOrDefault(c => c.Id == request.id);

                if (commissionAndDiscount == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کارمزد و تخفیف مشاور یافت نشد!!"
                    };
                }

                commissionAndDiscount.PhoneCallCommission = Convert.ToDouble(request.phoneCallCommission);
                commissionAndDiscount.VoiceCallCommission = Convert.ToDouble(request.voiceCallCommission);
                commissionAndDiscount.TextCallCommission = Convert.ToDouble(request.textCallCommission);
                commissionAndDiscount.PhoneCallDiscount = Convert.ToDouble(request.phoneCallDiscount);
                commissionAndDiscount.VoiceCallDiscount = Convert.ToDouble(request.voiceCallDiscount);
                commissionAndDiscount.TextCallDiscount = Convert.ToDouble(request.textCallDiscount);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
