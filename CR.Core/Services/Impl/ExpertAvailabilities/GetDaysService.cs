using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.ExpertAvailabilities
{
    public class GetDaysService : IGetDaysService
    {
        private readonly ApplicationContext _context;

        public GetDaysService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetDaysDto> Execute(long expertId)
        {
            var expertInformation = _context.ExpertInformations.FirstOrDefault(e => e.ExpertId == expertId);

            if (expertInformation == null)
            {
                return new ResultDto<ResultGetDaysDto>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد"
                };
            }

            var days = _context.Days
                .Include(d => d.TimeOfDays)
                .Where(d => d.ExpertInformationId == expertInformation.Id
                            && d.Date.Date >= DateTime.Now.Date
                            && d.Date.Date < DateTime.Now.AddDays(31).Date)
                .OrderBy(d => d.Date)
                .Select(d => new DayDto
                {
                    dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                    date_String = d.Date_String,
                    id = d.Id,
                    timeOfDayDtos = d.TimeOfDays.Where(t => t.IsReserved == false).Select(f => new TimeOfDayDto()
                    {
                        id = f.Id,
                        expertInformationId = f.ExpertInformationId,
                        dayId = f.DayId,
                        start = f.StartHour,
                        finish = f.FinishHour
                    }).ToList(),
                }).ToList();

            return new ResultDto<ResultGetDaysDto>()
            {
                Data = new ResultGetDaysDto
                {
                    DayDtos = days,
                    ExpertInformationId = expertInformation.Id
                },
                IsSuccess = true
            };
        }
    }
}
