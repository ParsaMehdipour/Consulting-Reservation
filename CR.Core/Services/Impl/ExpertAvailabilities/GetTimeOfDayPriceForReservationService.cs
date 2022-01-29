using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Impl.ExpertAvailabilities
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
                commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.PhoneCallCommission * timeOfDay.PhoneCallPrice) / 100);
                discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.PhoneCallDiscount * timeOfDay.PhoneCallPrice) / 100);
                price = ((timeOfDay.PhoneCallPrice + commission) - discount);
            }


            if (callingType == CallingType.VoiceCall)
            {
                commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.VoiceCallCommission * timeOfDay.PhoneCallPrice) / 100);
                discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.VoiceCallDiscount * timeOfDay.PhoneCallPrice) / 100);
                price = ((timeOfDay.VoiceCallPrice + commission) - discount);
            }

            if (callingType == CallingType.TextCall)
            {
                commission = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.TextCallDiscount * timeOfDay.PhoneCallPrice) / 100);
                discount = (long)((timeOfDay.ExpertInformation.CommissionAndDiscount.TextCallDiscount * timeOfDay.PhoneCallPrice) / 100);
                price = ((timeOfDay.TextCallPrice + commission) - discount);
            }

            return new ResultDto<long>()
            {
                Data = price,
                IsSuccess = true
            };
        }
    }
}
