using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetExpertsForPresentationService : IGetExpertsForPresentationService
    {
        private readonly ApplicationContext _context;

        public GetExpertsForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<ExpertForPresentationDto>> Execute()
        {
            var experts = _context.Users
                .Include(e => e.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Where(e => e.UserFlag == UserFlag.Expert && e.IsActive == true)
                .Select(e => new ExpertForPresentationDto
                {
                    FirstAvailability = "HardCode",
                    FullName = e.ExpertInformation.FirstName + " " + e.ExpertInformation.LastName,
                    IconSrc = e.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = e.Id,
                    Speciality = e.ExpertInformation.Specialty.Name,
                    SpecialitySrc = e.ExpertInformation.Specialty.ImageSrc,
                    Tags = e.ExpertInformation.Tag,
                    ExpertInformationId = e.ExpertInformation.Id
                }).OrderByDescending(e => e.Id).Take(10).ToList();

            return new ResultDto<List<ExpertForPresentationDto>>()
            {
                Data = experts,
                IsSuccess = true
            };
        }
    }
}
