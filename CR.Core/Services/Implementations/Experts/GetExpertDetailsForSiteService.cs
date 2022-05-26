using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Days;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetExpertDetailsForSiteService : IGetExpertDetailsForSiteService
    {
        private readonly ApplicationContext _context;

        public GetExpertDetailsForSiteService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertDetailsForSiteDto> Execute(long expertInformationId)
        {
            var expertInformation = _context.ExpertInformations
                .Include(e => e.ExpertExperiences)
                .Include(e => e.ExpertMemberships)
                .Include(e => e.ExpertPrizes)
                .Include(e => e.ExpertStudies)
                .Include(e => e.ExpertSubscriptions)
                .Include(e => e.Specialty)
                .Include(e => e.Days)
                .ThenInclude(e => e.TimeOfDays)
                .Include(_ => _.Favorites)
                .Include(e => e.ExpertAppointments)
                .FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertDetailsForSiteDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات مشاور یافت نشد"
                };
            }

            var expertDetailsForSite = new ExpertDetailsForSiteDto()
            {
                id = expertInformation.Id,
                IconSrc = (string.IsNullOrWhiteSpace(expertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : expertInformation.IconSrc,
                HasStar = (expertInformation.Favorites.Count >= 2),
                City = expertInformation.City,
                Province = expertInformation.Province,
                FullName = expertInformation.FirstName + " " + expertInformation.LastName,
                RateCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.CommentStatus == CommentStatus.Accepted && _.OwnerRecordId == expertInformation.Id),
                AverageRate = Decimal.Round(expertInformation.AverageRate),
                ClinicAddress = expertInformation.ClinicAddress,
                ClinicName = expertInformation.ClinicName,
                Tag = expertInformation.Tag,
                Tags = (string.IsNullOrEmpty(expertInformation.Tag)) ? new List<string>() : expertInformation.Tag.Split(",").ToList(),
                SpecialityImage = expertInformation.Specialty.ImageSrc,
                //YearsOfExperience = expertInformation.ExpertExperiences.Count > 0 ? Convert.ToInt32(expertInformation.ExpertExperiences.Last().FinishYear) - Convert.ToInt32(expertInformation.ExpertExperiences.First().StartYear) : 0,
                YearsOfExperience = expertInformation.ExpertExperiences.Count > 0 ? expertInformation.ExpertExperiences.Max(a => Convert.ToInt32(a.FinishYear)) - expertInformation.ExpertExperiences.Min(a => Convert.ToInt32(a.StartYear)) : 0,
                NumberOfDoneAppointments = expertInformation.ExpertAppointments.Count(_ => _.AppointmentStatus == AppointmentStatus.Completed),
                Speciality = (expertInformation.Specialty != null) ? expertInformation.Specialty.Name : " ",
                DayDtos = expertInformation.Days
                    .Where(d => d.Date >= DateTime.Now.Date && d.Date <= DateTime.Now.Date.AddDays(7))
                    .Select(d => new DayForExpertDetailsDto()
                    {
                        date_String = d.Date_String,
                        dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                        HasTimeOfDay = d.TimeOfDays.Count > 0,
                        StartHour = d.TimeOfDays.FirstOrDefault()?.StartHour,
                        FinishHour = d.TimeOfDays.LastOrDefault()?.FinishHour
                    }).ToList(),
                memberships = (expertInformation.ExpertMemberships == null) ? new List<ExpertMembershipDto>() : expertInformation.ExpertMemberships.Select(e => new ExpertMembershipDto
                {
                    membershipName = e.Name
                }).ToList(),
                prizes = (expertInformation.ExpertPrizes == null) ? new List<ExpertPrizeDto>() : expertInformation.ExpertPrizes.Select(e => new ExpertPrizeDto
                {
                    prizeName = e.PrizeName,
                    year = e.Year
                }).ToList(),
                studies = (expertInformation.ExpertStudies == null) ? new List<ExpertStudyDto>() : expertInformation.ExpertStudies.Select(e => new ExpertStudyDto
                {
                    degreeOfEducation = e.DegreeOfEducation,
                    endDate = e.EndDate,
                    university = e.University
                }).ToList(),
                subscriptions = (expertInformation.ExpertSubscriptions == null) ? new List<ExpertSubscriptionDto>() : expertInformation.ExpertSubscriptions.Select(e => new ExpertSubscriptionDto
                {
                    subscriptionName = e.SubscriptionName,
                    subscriptionYear = e.Year
                }).ToList(),
            };

            return new ResultDto<ExpertDetailsForSiteDto>()
            {
                Data = expertDetailsForSite,
                IsSuccess = true
            };
        }
    }
}
