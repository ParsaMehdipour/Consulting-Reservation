using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Impl.Experts
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
                .FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertDetailsForSiteDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد"
                };
            }

            var expertDetailsForSite = new ExpertDetailsForSiteDto()
            {
                id = expertInformation.Id,
                IconSrc = (string.IsNullOrWhiteSpace(expertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : expertInformation.IconSrc,
                Bio = expertInformation.Bio,
                City = expertInformation.City,
                Province = expertInformation.Province,
                //Price = (expertInformation.IsFreeOfCharge == true) ? 0 : expertInformation.Price,
                FullName = expertInformation.FirstName + " " + expertInformation.LastName,
                Rate = 4,
                ClinicAddress = expertInformation.ClinicAddress,
                ClinicName = expertInformation.ClinicName,
                RateCount = 10,
                Tag = expertInformation.Tag,
                Tags = (string.IsNullOrEmpty(expertInformation.Tag)) ? new List<string>() : expertInformation.Tag.Split(",").ToList(),
                SpecialityImage = expertInformation.Specialty.ImageSrc,
                Speciality = (expertInformation.Specialty != null) ? expertInformation.Specialty.Name : " ",
                DayDtos = expertInformation.Days
                    .Where(d => d.Date >= DateTime.Now.Date && d.Date < DateTime.Now.Date.AddDays(7))
                    .Select(d => new DayDto()
                    {
                        date_String = d.Date_String,
                        dayOfWeek = DateConvertor.GetDayOfWeek(d.Date),
                        id = d.Id,
                        timeOfDayDtos = d.TimeOfDays.Select(f => new TimeOfDayDto()
                        {
                            id = f.Id,
                            expertInformationId = f.ExpertInformationId,
                            dayId = f.DayId
                        }).ToList(),
                    }).ToList(),
                experiences = (expertInformation.ExpertExperiences == null) ? new List<ExpertExperienceDto>() : expertInformation.ExpertExperiences.Select(e => new ExpertExperienceDto
                {
                    clinicName = e.ClinicName,
                    startYear = e.StartYear,
                    finishYear = e.FinishYear,
                    role = e.Role
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
