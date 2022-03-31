using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetAllExpertsService : IGetAllExpertsService
    {
        private readonly ApplicationContext _context;

        public GetAllExpertsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllExpertsDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var experts = _context.Users
                .Include(u => u.ExpertInformation)
                .ThenInclude(u => u.Specialty)
                .Include(u => u.ExpertInformation)
                .ThenInclude(u => u.ExpertAppointments)
                .Where(u => u.UserFlag == UserFlag.Expert)
                .OrderByDescending(e => e.Id)
                .AsNoTracking()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .Select(u => new ExpertForAdminDto
                {
                    Id = u.Id,
                    FullName = u.ExpertInformation.FirstName + " " + u.ExpertInformation.LastName,
                    IconSrc = u.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Income = _context.FinancialTransactions.Where(_ => _.TransactionType == TransactionType.ChargeExpertWallet).Sum(_ => _.Price_Digit).ToString("n0"),
                    IsActive = u.IsActive,
                    RegisterDate = u.CreationDate.ToShamsi(),
                    Speciality = (u.ExpertInformation.Specialty != null) ? u.ExpertInformation.Specialty.Name : "تخصص نامعلوم",
                    PhoneNumber = u.UserName
                }).ToList();

            return new ResultDto<ResultGetAllExpertsDto>()
            {
                Data = new ResultGetAllExpertsDto()
                {
                    Experts = experts,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
