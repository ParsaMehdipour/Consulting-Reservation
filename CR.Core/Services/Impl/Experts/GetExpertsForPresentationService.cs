using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Experts
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
                    IconSrc = e.ExpertInformation.IconSrc,
                    Id = e.Id,
                    Price = (e.ExpertInformation.IsFreeOfCharge == true)
                        ? "0"
                        : e.ExpertInformation.Price.ToString().GetPersianNumber(),
                    Speciality = e.ExpertInformation.Specialty.Name,
                    SpecialitySrc = e.ExpertInformation.Specialty.ImageSrc,
                    Tags = e.ExpertInformation.Tag,
                    ExpertInformationId = e.ExpertInformation.Id
                }).OrderByDescending(e=>e.Id).Take(10).ToList();

            return new ResultDto<List<ExpertForPresentationDto>>()
            {
                Data = experts,
                IsSuccess = true
            };
        }
    }
}
