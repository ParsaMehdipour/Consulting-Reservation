using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Experts
{
    public class GetExpertDetailsForReservationService : IGetExpertDetailsForReservationService
    {
        private readonly ApplicationContext _context;

        public GetExpertDetailsForReservationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertDetailsForReservationDto> Execute(long expertInformationId)
        {
            var expertInformation = _context.ExpertInformations
                .Include(e => e.Specialty)
                .Include(e => e.Days)
                .ThenInclude(e => e.TimeOfDays)
                .ThenInclude(t=>t.Timing)
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
                ExpertInformationId = expertInformation.Id,
                IconSrc = (string.IsNullOrWhiteSpace(expertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : expertInformation.IconSrc,
                usePhoneCall = expertInformation.UsePhoneCall,
                useVoiceCall = expertInformation.UseVoiceCall,
                useTextCall = expertInformation.UseTextCall,
                phonePrice = expertInformation.UsePhoneCall ? expertInformation.PhoneCallPrice : 0,
                voicePrice = expertInformation.UseVoiceCall ? expertInformation.VoiceCallPrice : 0,
                textPrice = expertInformation.UseTextCall ? expertInformation.TextCallPrice : 0,
                FullName = expertInformation.FirstName + " " + expertInformation.LastName,
                Rate = 4,
                RateCount = 10,
                SpecialitySrc = expertInformation.Specialty.ImageSrc,
                Speciality = (expertInformation.Specialty != null) ? expertInformation.Specialty.Name : " ",
                DayDtos = expertInformation.Days
                    .Where(d => d.Date >= DateTime.Now.Date && d.Date < DateTime.Now.Date.AddDays(7))
                    .Select(d => new DayDto()
                    {
                        date_String = d.Date_String,
                        dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                        id = d.Id,
                        TimeOfDayDtos = d.TimeOfDays.Where(t=>t.IsReserved == false).Select(f => new TimeOfDayDto()
                        {
                            id = f.Id,
                            expertInformationId = f.ExpertInformationId,
                            dayId = f.DayId,
                            start = f.Timing.StartTime_String,
                            finish = f.Timing.EndTime_String
                        }).OrderBy(t=>t.start).ToList(),
                    }).OrderBy(d=>d.date_String.ToGeorgianDateTime()).ToList()
            };

            return new ResultDto<ExpertDetailsForReservationDto>()
            {
                Data = expertDetailsForReservation,
                IsSuccess = true
            };
        }
    }
}
