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
    public class GetLatestExpertsForAdminService : IGetLatestExpertsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetLatestExpertsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<LatestExpertForAdminDto>> Execute()
        {
            var latestExperts = _context.Users
                .Include(u => u.ExpertInformation)
                .ThenInclude(u => u.Specialty)
                .Include(u => u.ExpertInformation.ExpertAppointments)
                .Where(u => u.UserFlag == UserFlag.Expert)
                .Select(u => new LatestExpertForAdminDto
                {
                    id = u.Id,
                    firstName = u.ExpertInformation.FirstName,
                    lastName = u.ExpertInformation.LastName,
                    speciality = u.ExpertInformation.Specialty.Name,
                    RateCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.CommentStatus == CommentStatus.Accepted && _.OwnerRecordId == u.ExpertInformation.Id),
                    AverageRate = Decimal.Round(u.ExpertInformation.AverageRate),
                    income = u.ExpertInformation.ExpertAppointments.Where(_ => _.AppointmentStatus == AppointmentStatus.NotDone || _.AppointmentStatus == AppointmentStatus.Completed).Sum(_ => _.Price).ToString(),
                    iconSrc = u.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    expertInformationId = u.ExpertInformationId.Value
                }).Take(5).ToList();

            return new ResultDto<List<LatestExpertForAdminDto>>()
            {
                Data = latestExperts,
                IsSuccess = true
            };
        }
    }
}
