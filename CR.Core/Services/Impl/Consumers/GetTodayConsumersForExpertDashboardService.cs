using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Consumers
{
    public class GetTodayConsumersForExpertDashboardService : IGetTodayConsumersForExpertDashboardService
    {
        private readonly ApplicationContext _context;

        public GetTodayConsumersForExpertDashboardService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumersForExpertDashboardDto> Execute(long expertId, int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            var consumers = _context.Appointments
                .Include(a=>a.TimeOfDay)
                .ThenInclude(a=>a.Day)
                .Include(a => a.ExpertInformation)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Where(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.Day.Date.Date == DateTime.Now.Date)
                .Select(a => new ConsumerForExpertDashboardDto()
                {
                    FullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentPrice = a.Price.ToString().GetPersianNumber(),
                    AppointmentStatus = "HardCode",
                    AppointmentTime = (a.TimeOfDay.StartDate.Hour.ToString().GetPersianNumber() + ":" + a.TimeOfDay.StartDate.Minute.ToString().GetPersianNumber()).ToString() +
                                      " - " + (a.TimeOfDay.FinishDate.Hour.ToString().GetPersianNumber() + ":" + a.TimeOfDay.FinishDate.Minute.ToString().GetPersianNumber()),
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ConsumerId = a.ConsumerInformation.ConsumerId,
                    ConsumerType = (_context.Appointments.Any(c=>c.ConsumerInformationId == a.ConsumerInformationId && a.TimeOfDay.Day.Date.Date < DateTime.Now.Date)) ? "مراجع قدیمی" : "مراجع جدید"
                }).OrderByDescending(a => a.AppointmentDate)
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();


            return new ResultDto<ResultGetConsumersForExpertDashboardDto>()
            {
                Data = new ResultGetConsumersForExpertDashboardDto()
                {
                    ConsumerForExpertDashboardDtos = consumers,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
