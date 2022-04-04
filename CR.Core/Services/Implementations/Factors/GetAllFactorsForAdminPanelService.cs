using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Factors;
using CR.Core.DTOs.ResultDTOs.Factors;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Factors
{
    public class GetAllFactorsForAdminPanelService : IGetAllFactorsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllFactorsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllFactorsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            var factors = _context.Factors
                .Include(f => f.Appointments)
                .Include(f => f.ConsumerInformation)
                .OrderByDescending(_ => _.CreateDate)
                .AsNoTracking()
                .Select(f => new FactorForAdminDto()
                {
                    ConsumerId = f.ConsumerInformation.ConsumerId,
                    ConsumerFullName = f.ConsumerInformation.FirstName + " " + f.ConsumerInformation.LastName,
                    ConsumerIconSrc = f.ConsumerInformation.IconSrc ?? "assets/img/User.png",
                    CreateDate = f.CreateDate.ToShamsi(),
                    FactorNumber = f.FactorNumber,
                    Status = f.FactorStatus.GetDisplayName(),
                    TotalPrice = f.TotalPrice.ToString("n0")
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetAllFactorsForAdminPanelDto>()
            {
                Data = new ResultGetAllFactorsForAdminPanelDto()
                {
                    FactorForAdminDtos = factors,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
