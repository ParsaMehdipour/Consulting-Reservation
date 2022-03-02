using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class GetDayDetailsService : IGetDayDetailsService
    {
        private readonly ApplicationContext _context;

        public GetDayDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<TimingForEditDto>> Execute(long dayId, long expertId)
        {
            var day = _context.Days
                .Include(d => d.TimeOfDays)
                .Include(d => d.ExpertInformation)
                .FirstOrDefault(d => d.Id == dayId && d.ExpertInformation.ExpertId == expertId);

            if (day == null)
            {
                return new ResultDto<List<TimingForEditDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "روز مورد نظر شما یافت نشد!!"
                };
            }

            var timings = new List<TimingForEditDto>();

            foreach (var timeOfDay in day.TimeOfDays)
            {
                var timing = new TimingForEditDto()
                {
                    timingDtos = _context.Timings.Where(_ => _.TimingType == timeOfDay.TimingType).Select(_ => new TimingDto()
                    {
                        startTime = _.StartTime_String,
                        endTime = _.EndTime_String,
                        id = _.Id,
                        timingType = _.TimingType,
                        IsSelected = (timeOfDay.StartHour == _.StartTime_String && timeOfDay.FinishHour == _.EndTime_String)
                    }).ToList(),
                    timingTypName = timeOfDay.TimingType.GetDisplayName()
                };

                timings.Add(timing);
            }

            return new ResultDto<List<TimingForEditDto>>()
            {
                Data = timings,
                IsSuccess = true
            };
        }
    }
}
