using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class GetDayDetailsService : IGetDayDetailsService
    {
        private readonly ApplicationContext _context;

        public GetDayDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<TimingDto>> Execute(long dayId)
        {
            var day = _context.Days
                .Include(d => d.TimeOfDays)
                .FirstOrDefault(d=>d.Id == dayId);

            if (day == null)
            {
                return new ResultDto<List<TimingDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "روز مورد نظر شما یافت نشد!!"
                };
            }

            var timings = new List<TimingDto>();

            foreach (var timeOfDay in day.TimeOfDays)
            {
                var timing = new TimingDto()
                {
                    startTime = timeOfDay.StartHour,
                    endTime = timeOfDay.FinishHour,
                    id = timeOfDay.Id,
                    timingType = timeOfDay.TimingType
                };

                timings.Add(timing);
            }

            return new ResultDto<List<TimingDto>>()
            {
                Data = timings,
                IsSuccess = true
            };
        }
    }
}
