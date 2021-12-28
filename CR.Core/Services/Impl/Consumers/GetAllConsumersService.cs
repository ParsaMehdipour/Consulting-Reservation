using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CR.Common.Utilities;

namespace CR.Core.Services.Impl.Consumers
{
    public class GetAllConsumersService : IGetAllConsumersService
    {
        private readonly ApplicationContext _context;

        public GetAllConsumersService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllConsumersDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var consumers = _context.Users
                .Include(u => u.ConsumerInfromation)
                .Where(u => u.ConsumerInfromation != null)
                .Where(u => u.UserFlag == UserFlag.Consumer).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .Select(u => new ConsumerForAdminDto
                {
                    Id = u.Id,
                    IconSrc = u.ConsumerInfromation.IconSrc ?? "assets/img/icon-256x256.png",
                    FullName = u.ConsumerInfromation.FirstName + " " + u.ConsumerInfromation.LastName,
                    Age = u.ConsumerInfromation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    Address = u.ConsumerInfromation.SpecificAddress,
                    PhoneNumber = u.PhoneNumber.GetPersianNumber(),
                    LastAppointment = "HardCode",
                    PaidAmount = "HardCode",
                }).ToList();

            return new ResultDto<ResultGetAllConsumersDto>
            {
                Data = new ResultGetAllConsumersDto
                {
                    Consumers = consumers,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
