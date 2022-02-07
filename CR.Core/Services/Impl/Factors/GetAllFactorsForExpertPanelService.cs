using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Factors;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CR.Core.DTOs.ResultDTOs.Factors;

namespace CR.Core.Services.Impl.Factors
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
            int rowCount = 0;

            var factors = _context.Factors
                .Include(f => f.ExpertInformation)
                .Include(f => f.Appointments)
                .Include(f => f.ConsumerInformation)
                .Where(f => f.ExpertInformation.ExpertId == expertId)
                .Select(f => new FactorForExpertPanelDto()
                {
                    ConsumerId = f.ConsumerInformation.ConsumerId,
                    ConsumerFullName = f.ConsumerInformation.FirstName + " " + f.ConsumerInformation.LastName,
                    ConsumerIconSrc = f.ConsumerInformation.IconSrc,
                    CreateDate = f.CreateDate.ToShamsi(),
                    FactorNumber = f.FactorNumber,
                    Status = f.FactorStatus.GetDisplayName(),
                    TotalPrice = f.TotalPrice.ToString("n0")
                }).ToList();

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
