using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.Factors;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Factors
{
    public class GetFactorDetailsService : IGetFactorDetailsService
    {
        private readonly ApplicationContext _context;

        public GetFactorDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<FactorDetailsForSiteDto> Execute(string factorNumber)
        {
            var factor = _context.Factors
                .Include(f => f.ConsumerInformation)
                .Include(f => f.ExpertInformation)
                .Include(a => a.Appointments)
                .ThenInclude(a => a.TimeOfDay)
                .ThenInclude(t => t.Day)
                .FirstOrDefault(f => f.FactorNumber == factorNumber);

            if (factor == null)
            {
                return new ResultDto<FactorDetailsForSiteDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات فاکتور یافت نشد!!"
                };
            }

            if (factor.Appointments.FirstOrDefault() == null)
            {
                return new ResultDto<FactorDetailsForSiteDto>()
                {
                    IsSuccess = false,
                    Message = "نوبتی در این فاکتور وجود ندارد!!",
                    Data = null
                };
            }

            var appointments = factor.Appointments.Select(a => new AppointmentDetailsForSiteDto()
            {
                CallingType = a.CallingType.GetDisplayName(),
                Date = a.TimeOfDay.Day.Date_String,
                Time = a.TimeOfDay.StartHour + " - " + a.TimeOfDay.FinishHour,
                TimingType = a.TimeOfDay.TimingType.GetDisplayName(),
                Price = a.Price ?? 0,
            }).ToList();

            return new ResultDto<FactorDetailsForSiteDto>()
            {
                Data = new FactorDetailsForSiteDto()
                {
                    Id = factor.Id,
                    RateCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.CommentStatus == CommentStatus.Accepted && _.OwnerRecordId == factor.ExpertInformationId),
                    AverageRate = Decimal.Round(factor.ExpertInformation.AverageRate),
                    AppointmentDetailsForSiteDtos = appointments,
                    price = appointments.Sum(a => a.Price),
                    expertFullName = factor.Appointments.FirstOrDefault()?.ExpertInformation.FirstName + " " + factor.Appointments.FirstOrDefault()?.ExpertInformation.LastName,
                    expertIconSrc = factor.Appointments.FirstOrDefault()?.ExpertInformation.IconSrc ?? "assets/img/User.png",
                    expertInformationId = factor.Appointments.FirstOrDefault()?.ExpertInformation.Id,
                    refId = factor.RefId,
                    consumerId = factor.ConsumerInformation.ConsumerId
                },
                IsSuccess = true,
            };
        }
    }
}
