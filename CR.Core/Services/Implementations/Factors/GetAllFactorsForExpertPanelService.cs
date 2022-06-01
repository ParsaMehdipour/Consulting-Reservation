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
    public class GetAllFactorsForExpertPanelService : IGetAllFactorsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllFactorsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllFactorsForExpertPanelService> Execute(long expertId, int Page = 1, int PageSize = 20)
        {
            var factors = _context.Factors
                .Include(f => f.ExpertInformation)
                .Include(f => f.Appointments)
                .Include(f => f.ConsumerInformation)
                .AsNoTracking()
                .Where(f => f.ExpertInformation.ExpertId == expertId)
                .Select(f => new FactorForExpertPanelDto()
                {
                    ConsumerId = f.ConsumerInformation.ConsumerId,
                    ConsumerFullName = f.ConsumerInformation.FirstName + " " + f.ConsumerInformation.LastName,
                    ConsumerIconSrc = f.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    CreateDate = f.CreateDate.ToShamsi(),
                    FactorNumber = f.FactorNumber,
                    Status = f.FactorStatus.GetDisplayName(),
                    TotalPrice = f.TotalPrice.ToString("n0")
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetAllFactorsForExpertPanelService>()
            {
                Data = new ResultGetAllFactorsForExpertPanelService()
                {
                    FactorForExpertPanelDtos = factors,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
