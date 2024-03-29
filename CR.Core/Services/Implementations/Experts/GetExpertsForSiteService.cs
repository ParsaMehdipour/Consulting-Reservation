﻿using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetExpertsForSiteService : IGetExpertsForSiteService
    {
        private readonly ApplicationContext _context;

        public GetExpertsForSiteService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertsForSiteDto> Execute(string searchKey, List<string> speciality, GenderType gender, int Page = 1, int PageSize = 20)
        {
            var expertsQuery = _context.Users
                .Include(u => u.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(e => e.ExpertInformation.Favorites)
                .Include(e => e.ExpertInformation.TimeOfDays)
                .Include(e => e.ExpertInformation.ExpertExperiences)
                .Include(e => e.ExpertInformation.ExpertAppointments)
                .Where(u => u.UserFlag == UserFlag.Expert && u.IsActive == true && u.ExpertInformation != null)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                expertsQuery = expertsQuery.Where(e => e.ExpertInformation.FirstName.Contains(searchKey)
                               || e.ExpertInformation.LastName.Contains(searchKey)
                               || e.ExpertInformation.Specialty.Name.Contains(searchKey)
                               || e.ExpertInformation.Tag.Contains(searchKey));
            }

            if (speciality.Count > 0)
            {
                foreach (var spec in speciality)
                {
                    expertsQuery = expertsQuery.Where(e => e.ExpertInformation.Specialty.Name.Contains(spec));
                }
            }

            if (gender != 0)
            {
                expertsQuery = expertsQuery.Where(e => e.ExpertInformation.Gender == gender);
            }

            var experts = expertsQuery.Select(e => new ExpertForSiteDto
            {
                IconSrc = (string.IsNullOrWhiteSpace(e.ExpertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : e.ExpertInformation.IconSrc,
                expertInformationId = e.ExpertInformation.Id,
                HasStar = (e.ExpertInformation.Favorites.Count >= 2),
                City = e.ExpertInformation.City,
                Province = e.ExpertInformation.Province,
                ClinicImages = new List<string>(),
                FullName = e.ExpertInformation.FirstName + " " + e.ExpertInformation.LastName,
                RateCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.CommentStatus == CommentStatus.Accepted && _.OwnerRecordId == e.ExpertInformation.Id),
                AverageRate = Decimal.Round(e.ExpertInformation.AverageRate),
                Speciality = e.ExpertInformation.Specialty.Name,
                HasTimeOfDays = e.ExpertInformation.TimeOfDays.Any(_ => _.StartTime.Date >= DateTime.Now.Date && (e.ExpertInformation.UsePhoneCall || e.ExpertInformation.UseVoiceCall || e.ExpertInformation.UseTextCall)),
                SpecialitySrc = e.ExpertInformation.Specialty.ImageSrc,
                Tags = !string.IsNullOrEmpty(e.ExpertInformation.Tag) ? e.ExpertInformation.Tag.Split(",", System.StringSplitOptions.None).ToList() : new List<string>(),
                PhoneCall = e.ExpertInformation.UsePhoneCall,
                VoiceCall = e.ExpertInformation.UseVoiceCall,
                TextCall = e.ExpertInformation.UseTextCall,
                YearsOfExperience = e.ExpertInformation.ExpertExperiences.Count > 0 ? e.ExpertInformation.ExpertExperiences.Max(a => Convert.ToInt32(a.FinishYear)) - e.ExpertInformation.ExpertExperiences.Min(a => Convert.ToInt32(a.StartYear)) : 0,
                NumberOfDoneAppointments = e.ExpertInformation.ExpertAppointments.Count(_ => _.AppointmentStatus == AppointmentStatus.Completed),
            }).AsEnumerable()
                .Randomize()
                .ToPaged(Page, PageSize, out var rowCount)
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
