using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.ResultDTOs.ExpertAvailabilities;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class GetExpertAvailabilitiesForReservationService : IGetExpertAvailabilitiesForReservationService
    {
        private readonly ApplicationContext _context;

        public GetExpertAvailabilitiesForReservationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertAvailabilitiesDetailsDto> Execute(long expertInformationId, TimingType timingType, DateTime date)
        {
            var expertInformation = _context.ExpertInformations
                .Include(e => e.Days)
                .ThenInclude(e => e.TimeOfDays)
                .FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<ResultGetExpertAvailabilitiesDetailsDto>()
                {
                    Data = new ResultGetExpertAvailabilitiesDetailsDto(),
                    IsSuccess = false,
                    Message = "اطلاعات مشاور یافت نشد"
                };
            }

            if (date.Date < DateTime.Now.Date)
            {
                var list2 = expertInformation.Days
                    .Where(d => d.Date >= DateTime.Now.Date && d.Date < DateTime.Now.Date.AddDays(7))
                    .Select(d => new DayDto()
                    {
                        date_String = d.Date_String,
                        dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                        id = d.Id,
                        timeOfDayDtos = d.TimeOfDays.Where(t => t.IsReserved == false && t.TimingType == timingType && t.StartTime > DateTime.Now).Select(
                            f => new TimeOfDayDto()
                            {
                                id = f.Id,
                                expertInformationId = f.ExpertInformationId,
                                dayId = f.DayId,
                                start = f.StartHour,
                                finish = f.FinishHour
                            }).OrderBy(t => t.start).ToList(),
                    }).OrderBy(d => d.date_String.ToGeorgianDateTime()).ToList();

                return new ResultDto<ResultGetExpertAvailabilitiesDetailsDto>()
                {
                    Data = new ResultGetExpertAvailabilitiesDetailsDto()
                    {
                        year = date.Year,
                        month = date.Month,
                        day = date.Day,
                        dayDtos = list2
                    },
                    IsSuccess = true
                };
            }

            if (date.Date > DateTime.Now.Date)
            {
                var list3 = expertInformation.Days
                    .Where(d => d.Date >= date.Date && d.Date < date.Date.AddDays(7))
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

                return new ResultDto<ResultGetExpertAvailabilitiesDetailsDto>()
                {
                    Data = new ResultGetExpertAvailabilitiesDetailsDto()
                    {
                        year = date.Year,
                        month = date.Month,
                        day = date.Day,
                        dayDtos = list3
                    },
                    IsSuccess = true
                };
            }

            var list = expertInformation.Days
                .Where(d => d.Date >= date.Date && d.Date < date.Date.AddDays(7))
                .Select(d => new DayDto()
                {
                    date_String = d.Date_String,
                    dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                    id = d.Id,
                    timeOfDayDtos = d.TimeOfDays.Where(t => t.IsReserved == false && t.TimingType == timingType && t.StartTime > date).Select(
                        f => new TimeOfDayDto()
                        {
                            id = f.Id,
                            expertInformationId = f.ExpertInformationId,
                            dayId = f.DayId,
                            start = f.StartHour,
                            finish = f.FinishHour
                        }).OrderBy(t => t.start).ToList(),
                }).OrderBy(d => d.date_String.ToGeorgianDateTime()).ToList();

            return new ResultDto<ResultGetExpertAvailabilitiesDetailsDto>()
            {
                Data = new ResultGetExpertAvailabilitiesDetailsDto()
                {
                    year = date.Year,
                    month = date.Month,
                    day = date.Day,
                    dayDtos = list
                },
                IsSuccess = true
            };
        }
    }
}
