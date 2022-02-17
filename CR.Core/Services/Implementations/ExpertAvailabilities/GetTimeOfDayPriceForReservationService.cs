using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class GetTimeOfDayPriceForReservationService : IGetTimeOfDayPriceForReservationService
    {
        private readonly ApplicationContext _context;

        public GetTimeOfDayPriceForReservationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<long> Execute(CallingType callingType, long timeOfDayId)
        {
            var timeOfDay = _context.TimeOfDays
                .Include(t => t.ExpertInformation)
                .ThenInclude(e => e.CommissionAndDiscount)
                .FirstOrDefault(t => t.Id == timeOfDayId);

            if (timeOfDay == null)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات نوبت یافت نشد!!",
                };
            }

            long price = 0;
            long commission = 0;
            long discount = 0;

            if (callingType == CallingType.PhoneCall)
            {
                if (timeOfDay.ExpertInformation.CommissionAndDiscount != null)
                {
                    commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.PhoneCallCommission * timeOfDay.PhoneCallPrice) / 100);
                    discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.PhoneCallDiscount * timeOfDay.PhoneCallPrice) / 100);
                    price = ((timeOfDay.PhoneCallPrice + commission) - discount);
                }
                else
                {
                    price = timeOfDay.PhoneCallPrice;
                }
            }


            if (callingType == CallingType.VoiceCall)
            {
                if (timeOfDay.ExpertInformation.CommissionAndDiscount != null)
                {
                    commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.VoiceCallCommission * timeOfDay.VoiceCallPrice) / 100);
                    discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.VoiceCallDiscount * timeOfDay.VoiceCallPrice) / 100);
                    price = ((timeOfDay.VoiceCallPrice + commission) - discount);
                }
                else
                {
                    price = timeOfDay.VoiceCallPrice;
                }
            }

            if (callingType == CallingType.TextCall)
            {
                if (timeOfDay.ExpertInformation.CommissionAndDiscount != null)
                {
                    commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.TextCallCommission * timeOfDay.TextCallPrice) / 100);
                    discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.TextCallDiscount * timeOfDay.TextCallPrice) / 100);
                    price = ((timeOfDay.TextCallPrice + commission) - discount);
                }
                else
                {
                    price = timeOfDay.TextCallPrice;
                }
            }

            return new ResultDto<long>()
            {
                Data = price,
                IsSuccess = true
            };
        }
    }
}
