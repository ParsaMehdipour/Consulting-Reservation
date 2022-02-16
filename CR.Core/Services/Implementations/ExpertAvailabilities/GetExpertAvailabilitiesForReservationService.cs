using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class GetExpertAvailabilitiesForReservationService : IGetExpertAvailabilitiesForReservationService
    {
        private readonly ApplicationContext _context;

        public GetExpertAvailabilitiesForReservationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<DayDto>> Execute(long expertInformationId, TimingType timingType)
        {
            var expertInformation = _context.ExpertInformations
                .Include(e => e.Days)
                .ThenInclude(e => e.TimeOfDays)
                .FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<List<DayDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد"
                };
            }

            var list = expertInformation.Days
                .Where(d => d.Date >= DateTime.Now.Date && d.Date < DateTime.Now.Date.AddDays(7))
                .Select(d => new DayDto()
                {
                    date_String = d.Date_String,
                    dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                    id = d.Id,
                    timeOfDayDtos = d.TimeOfDays.Where(t => t.IsReserved == false && t.TimingType == timingType).Select(
                        f => new TimeOfDayDto()
                        {
                            id = f.Id,
                            expertInformationId = f.ExpertInformationId,
                            dayId = f.DayId,
                            start = f.StartHour,
                            finish = f.FinishHour
                        }).OrderBy(t => t.start).ToList(),
                }).OrderBy(d => d.date_String.ToGeorgianDateTime()).ToList();

            return new ResultDto<List<DayDto>>()
            {
                Data = list,
                IsSuccess = true
            };
        }
    }
}
