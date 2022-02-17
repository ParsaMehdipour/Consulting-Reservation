using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetConsumersForExpertPanelService : IGetConsumersForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetConsumersForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumersForExpertPanelDto> Execute(long expertId,int Page = 1, int PageSize = 20)
        {

            int rowCount = 0;

            var consumers = _context.Appointments
                .Include(a => a.ExpertInformation)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Where(a => a.ExpertInformation.ExpertId == expertId)
                .Select(a => new ConsumerForExpertPanelDto
                {
                    Age = a.ConsumerInformation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    City = a.ConsumerInformation.City,
                    Province = a.ConsumerInformation.Province,
                    FullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    Gender = a.ConsumerInformation.Gender.GetDisplayName(),
                    IconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = a.ConsumerInformation.ConsumerId,
                    PhoneNumber = a.ConsumerInformation.Consumer.PhoneNumber.GetPersianNumber()

                }).OrderByDescending(a=>a.Id).Distinct()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();


            return new ResultDto<ResultGetConsumersForExpertPanelDto>()
            {
                Data = new ResultGetConsumersForExpertPanelDto()
                {
                    ConsumerForExpertPanelDtos = consumers,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
