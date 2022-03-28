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
                .ThenInclude(u => u.ConsumerAppointments)
                .Where(u => u.UserFlag == UserFlag.Consumer)
                .OrderByDescending(c => c.Id)
                .AsNoTracking()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .Select(u => new ConsumerForAdminDto
                {
                    Id = u.Id,
                    IconSrc = u.ConsumerInfromation?.IconSrc ?? "assets/img/icon-256x256.png",
                    FullName = u.ConsumerInfromation?.FirstName + " " + u.ConsumerInfromation?.LastName,
                    Age = u.ConsumerInfromation?.BirthDate.GetAge().ToString().GetPersianNumber(),
                    Address = u.ConsumerInfromation?.SpecificAddress,
                    PhoneNumber = u.PhoneNumber.GetPersianNumber(),
                    LastAppointment = (u.ConsumerInfromation != null) ? _context.Appointments
                        .Include(a => a.ConsumerInformation)
                        .Include(a => a.TimeOfDay)
                        .ThenInclude(a => a.Day)
                        .OrderBy(a => a.TimeOfDay.Day.Date)
                        .LastOrDefault(a => a.ConsumerInformation.ConsumerId == u.Id)
                        ?.TimeOfDay.Day.Date_String : "",
                    PaidAmount = (u.ConsumerInfromation != null) ? u.ConsumerInfromation.ConsumerAppointments.Sum(a => a.Price.Value).ToString("n0") : "0",
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
