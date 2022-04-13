using CR.Common.DTOs;
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
                .Include(e => e.ExpertInformation)
                .ThenInclude(_ => _.Favorites)
                .Where(e => e.UserFlag == UserFlag.Expert && e.IsActive == true && e.ExpertInformation.Favorites.Count > 0)
                .OrderByDescending(e => e.Favorites.Count)
                .Select(e => new ExpertForPresentationDto
                {
                    FirstAvailability = "HardCode",
                    FullName = e.ExpertInformation.FirstName + " " + e.ExpertInformation.LastName,
                    IconSrc = e.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = e.Id,
                    Speciality = e.ExpertInformation.Specialty.Name,
                    SpecialitySrc = e.ExpertInformation.Specialty.ImageSrc,
                    Tags = e.ExpertInformation.Tag,
                    ExpertInformationId = e.ExpertInformation.Id,
                    HasStar = (e.ExpertInformation.Favorites.Count >= 2),
                    AverageRate = Decimal.Round(e.ExpertInformation.AverageRate),
                    RateCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.CommentStatus == CommentStatus.Accepted && _.OwnerRecordId == e.ExpertInformation.Id)
                }).Take(10).ToList();

            return new ResultDto<List<ExpertForPresentationDto>>()
            {
                Data = experts,
                IsSuccess = true
            };
        }
    }
}
