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
                .Where(u => u.UserFlag == UserFlag.Expert)
                .Select(u => new LatestExpertForAdminDto
                {
                    id = u.Id,
                    firstName = u.ExpertInformation.FirstName,
                    lastName = u.ExpertInformation.LastName,
                    speciality = u.ExpertInformation.Specialty.Name,
                    income = "HardCode"
                }).Take(5).ToList();

            return new ResultDto<List<LatestExpertForAdminDto>>()
            {
                Data = latestExperts,
                IsSuccess = true
            };
        }
    }
}
