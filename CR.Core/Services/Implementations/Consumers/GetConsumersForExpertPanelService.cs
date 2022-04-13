using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetConsumersForExpertPanelService : IGetConsumersForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetConsumersForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumersForExpertPanelDto> Execute(long expertId, int Page = 1, int PageSize = 20)
        {
            var consumers = _context.Appointments
                .Include(a => a.ExpertInformation)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Include(a => a.ConsumerInformation.ConsumerAppointments)
                .Where(a => a.ExpertInformation.ExpertId == expertId)
                .Select(a => new ConsumerForExpertPanelDto
                {
                    Age = a.ConsumerInformation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    FullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    Gender = a.ConsumerInformation.Gender.GetDisplayName(),
                    IconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = a.ConsumerInformation.ConsumerId,
                    PhoneNumber = (a.ConsumerInformation.ConsumerAppointments.Any(_ => _.CallingType == CallingType.PhoneCall)) ? a.ConsumerInformation.Consumer.PhoneNumber : null,
                    Degree = a.ConsumerInformation.Degree

                }).OrderByDescending(a => a.Id).Distinct()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
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
