using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.CommissionAndDiscounts;

namespace CR.Core.Services.Implementations.CommissionAndDiscounts
{
    public class AddNewCommissionAndDiscountService : IAddNewCommissionAndDiscountService
    {
        private readonly ApplicationContext _context;

        public AddNewCommissionAndDiscountService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewCommissionAndDiscountDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.Id == request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات متخصص یافت نشد!!"
                    };
                }

                var commissionAndDiscount = new CommissionAndDiscount()
                {
                    PhoneCallCommission = Convert.ToDouble(request.phoneCallCommission),
                    VoiceCallCommission = Convert.ToDouble(request.voiceCallCommission),
                    TextCallCommission = Convert.ToDouble(request.textCallCommission),
                    PhoneCallDiscount = Convert.ToDouble(request.phoneCallDiscount),
                    VoiceCallDiscount = Convert.ToDouble(request.voiceCallDiscount),
                    TextCallDiscount = Convert.ToDouble(request.textCallDiscount),
                    ExpertInformation = expertInformation
                };

                _context.CommissionAndDiscounts.Add(commissionAndDiscount);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تغیرات شما با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
