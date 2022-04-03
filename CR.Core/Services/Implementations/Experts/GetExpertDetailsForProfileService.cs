using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetExpertDetailsForProfileService : IGetExpertDetailsForProfileService
    {
        private readonly ApplicationContext _context;

        public GetExpertDetailsForProfileService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertDetailsForProfileDto> Execute(long expertId)
        {
            var expert = _context.Users.FirstOrDefault(u => u.Id == expertId);

            if (expert == null)
            {
                return new ResultDto<ExpertDetailsForProfileDto>()
                {
                    Data = new ExpertDetailsForProfileDto(),
                    IsSuccess = false,
                    Message = "اطلاعات شما یافت نشد!!"
                };
            }

            var expertInformation = _context.ExpertInformations
                .Include(e => e.Specialty)
                .Include(e => e.ExpertExperiences)
                .Include(e => e.ExpertImages)
                .Include(e => e.ExpertMemberships)
                .Include(e => e.ExpertPrizes)
                .Include(e => e.ExpertStudies)
                .Include(e => e.ExpertSubscriptions)
                .FirstOrDefault(e => e.ExpertId == expertId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertDetailsForProfileDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات مشاور پیدا نشد !!"
                };
            }

            return new ResultDto<ExpertDetailsForProfileDto>()
            {
                Data = new ExpertDetailsForProfileDto()
                {
                    id = expertInformation.Id,
                    birthDate = expertInformation.BirthDate,
                    userName = expert.UserName,
                    phoneNumber = expert.PhoneNumber.GetPersianNumber(),
                    bio = expertInformation.Bio,
                    instagram = expertInformation.Instagram,
                    birthDate_String = expertInformation.BirthDate_String,
                    city = expertInformation.City,
                    clinicAddress = expertInformation.ClinicAddress,
                    clinicName = expertInformation.ClinicName,
                    email = expert.Email,
                    gender = expertInformation.Gender,
                    iconSrc = expertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    province = expertInformation.Province,
                    postalCode = expertInformation.PostalCode.GetPersianNumber(),
                    specificAddress = expertInformation.SpecificAddress,
                    firstName = expertInformation.FirstName,
                    lastName = expertInformation.LastName,
                    specialtyId = expertInformation.Specialty?.Id ?? 0,
                    usePhoneCall = expertInformation.UsePhoneCall,
                    phoneCallPrice = expertInformation.UsePhoneCall ? expertInformation.PhoneCallPrice.ToString() : 0.ToString().GetPersianNumber(),
                    useVoiceCall = expertInformation.UseVoiceCall,
                    voiceCallPrice = expertInformation.UseVoiceCall ? expertInformation.VoiceCallPrice.ToString() : 0.ToString().GetPersianNumber(),
                    useTextCall = expertInformation.UseTextCall,
                    textCallPrice = expertInformation.UseTextCall ? expertInformation.TextCallPrice.ToString() : 0.ToString().GetPersianNumber(),
                    tag = expertInformation.Tag,
                    shabaNumber = expertInformation.ShabaNumber,
                    Tags = (string.IsNullOrEmpty(expertInformation.Tag)) ? new List<string>() : expertInformation.Tag.Split(",").ToList(),
                    otherImages = (expertInformation.ExpertImages == null) ? new List<ExpertImageDto>() : expertInformation.ExpertImages
                        .Where(i => i.ImageType == ImageType.Other)
                        .Select(i => new ExpertImageDto
                        {
                            Id = i.Id,
                            Src = i.Src
                        }
                    ).ToList(),
                    degreeImages = (expertInformation.ExpertImages == null) ? new List<ExpertImageDto>() : expertInformation.ExpertImages
                        .Where(i => i.ImageType == ImageType.Degree)
                        .Select(i => new ExpertImageDto
                        {
                            Id = i.Id,
                            Src = i.Src
                        }
                        ).ToList(),
                    resumeImages = (expertInformation.ExpertImages == null) ? new List<ExpertImageDto>() : expertInformation.ExpertImages
                        .Where(i => i.ImageType == ImageType.Resume)
                        .Select(i => new ExpertImageDto
                        {
                            Id = i.Id,
                            Src = i.Src
                        }
                        ).ToList(),
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
                },
                IsSuccess = true
            };


        }
    }
}
