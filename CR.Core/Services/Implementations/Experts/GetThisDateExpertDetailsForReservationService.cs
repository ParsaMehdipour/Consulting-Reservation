﻿using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetThisDateExpertDetailsForReservationService : IGetThisDateExpertDetailsForReservationService
    {
        private readonly ApplicationContext _context;

        public GetThisDateExpertDetailsForReservationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertDetailsForReservationDto> Execute(long expertInformationId, DateTime date)
        {
            var expertInformation = _context.ExpertInformations
                .Include(e => e.Specialty)
                .Include(e => e.Days)
                .ThenInclude(e => e.TimeOfDays)
                .FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertDetailsForReservationDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد"
                };
            }

            var expertDetailsForReservation = new ExpertDetailsForReservationDto()
            {
                ThisDate = date,
                ExpertInformationId = expertInformation.Id,
                IconSrc = (string.IsNullOrWhiteSpace(expertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : expertInformation.IconSrc,
                phonePrice = expertInformation.UsePhoneCall ? expertInformation.PhoneCallPrice : 0,
                voicePrice = expertInformation.UseVoiceCall ? expertInformation.VoiceCallPrice : 0,
                textPrice = expertInformation.UseTextCall ? expertInformation.TextCallPrice : 0,
                FullName = expertInformation.FirstName + " " + expertInformation.LastName,
                Rate = 4,
                RateCount = 10,
                SpecialitySrc = expertInformation.Specialty.ImageSrc,
                Speciality = (expertInformation.Specialty != null) ? expertInformation.Specialty.Name : " ",
                DayDtos = expertInformation.Days
                    .Where(d => d.Date >= date.Date && d.Date < date.Date.AddDays(7))
                    .Select(d => new DayDto()
                    {
                        date_String = d.Date_String,
                        dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                        id = d.Id,
                        timeOfDayDtos = d.TimeOfDays.Where(t => t.IsReserved == false).Select(f => new TimeOfDayDto()
                        {
                            id = f.Id,
                            expertInformationId = f.ExpertInformationId,
                            dayId = f.DayId,
                            start = f.StartHour,
                            finish = f.FinishHour
                        }).OrderBy(t => t.start).ToList(),
                    }).OrderBy(d => d.date_String.ToGeorgianDateTime()).ToList()
            };

            return new ResultDto<ExpertDetailsForReservationDto>()
            {
                Data = expertDetailsForReservation,
                IsSuccess = true
            };
        }
    }
}