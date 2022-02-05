using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Experts
{
    public class GetExpertsForSiteService : IGetExpertsForSiteService
    {
        private readonly ApplicationContext _context;

        public GetExpertsForSiteService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertsForSiteDto> Execute(string searchKey, string speciality,GenderType gender, int Page = 1, int PageSize = 20)
        {

            int rowCount = 0;
            var expertsQuery = _context.Users
                .Include(u => u.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Where(u => u.UserFlag == UserFlag.Expert
                 && u.IsActive == true
                 && u.ExpertInformation != null)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                expertsQuery = expertsQuery.Where(e => e.ExpertInformation.FirstName.Contains(searchKey)
                               || e.ExpertInformation.LastName.Contains(searchKey)
                               || e.ExpertInformation.Specialty.Name.Contains(searchKey)
                               || e.ExpertInformation.Tag.Contains(searchKey));
            }

            if (!string.IsNullOrWhiteSpace(speciality))
            {
                expertsQuery = expertsQuery.Where(e => e.ExpertInformation.Specialty.Name.Contains(speciality));
            }

            if (gender != 0)
            {
                expertsQuery = expertsQuery.Where(e => e.ExpertInformation.Gender == gender);
            }

            var experts = expertsQuery.Select(e => new ExpertForSiteDto
            {
                IconSrc = (string.IsNullOrWhiteSpace(e.ExpertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : e.ExpertInformation.IconSrc,
                expertInformationId = e.ExpertInformation.Id,
                Bio = e.ExpertInformation.Bio,
                City = e.ExpertInformation.City,
                Province = e.ExpertInformation.Province,
                ClinicImages = new List<string>(),
                FullName = e.ExpertInformation.FirstName + " " + e.ExpertInformation.LastName,
                //Price = (e.ExpertInformation.IsFreeOfCharge == true) ? 0 : e.ExpertInformation.Price,
                Rate = 5,
                RateCount = 10,
                Speciality = e.ExpertInformation.Specialty.Name,
                HasTimeOfDays = e.ExpertInformation.Days.Any(),
                SpecialitySrc = e.ExpertInformation.Specialty.ImageSrc,
                Tags = e.ExpertInformation.Tag.Split(",", System.StringSplitOptions.None).ToList()
            }).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetExpertsForSiteDto>()
            {
                Data = new ResultGetExpertsForSiteDto()
                {
                    ExpertForSiteDtos = experts,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true,
            };
        }
    }
}
