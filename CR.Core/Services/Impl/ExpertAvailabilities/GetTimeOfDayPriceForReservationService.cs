using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
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
            var timeOfDay = _context.TimeOfDays.FirstOrDefault(t => t.Id == timeOfDayId);

            if (timeOfDay == null)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات نوبت یافت نشد!!",
                };
            }

            long price = 0;

            if (callingType == CallingType.PhoneCall)
                price = timeOfDay.PhoneCallPrice;

            if (callingType == CallingType.VoiceCall)
                price = timeOfDay.VoiceCallPrice;

            if (callingType == CallingType.TextCall)
                price = timeOfDay.TextCallPrice;

            return new ResultDto<long>()
            {
                Data = price,
                IsSuccess = true
            };
        }
    }
}
