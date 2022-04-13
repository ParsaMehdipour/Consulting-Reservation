using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Appointment
{
    public class GetAllAppointmentsForExpertPanelService : IGetAllAppointmentsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllAppointmentsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllAppointmentsForExpertPanelDto> Execute(long expertId, int Page = 1, int PageSize = 20)
        {
            var appointmentsForExpertPanel = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a => a.ExpertInformation.ExpertId == expertId && (a.AppointmentStatus == AppointmentStatus.Declined
                                                                         || a.AppointmentStatus == AppointmentStatus.NotDone
                                                                         || a.AppointmentStatus == AppointmentStatus.Completed
                                                                         || a.AppointmentStatus == AppointmentStatus.Waiting))
                .OrderByDescending(a => a.TimeOfDay.Day.Date)
                .Select(a => new AppointmentForExpertPanelDto
                {
                    Id = a.Id,
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentStatus = a.AppointmentStatus.GetDisplayName(),
                    AppointmentTime = a.TimeOfDay.StartHour + " - " + a.TimeOfDay.FinishHour,
                    City = a.ConsumerInformation.City,
                    Province = a.ConsumerInformation.Province,
                    ConsumerFullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ConsumerId = a.ConsumerInformation.ConsumerId,
                    Email = a.ConsumerInformation.Consumer.Email,
                    PhoneNumber = a.ConsumerInformation.Consumer.PhoneNumber.ToString().GetPersianNumber()
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetAllAppointmentsForExpertPanelDto>()
            {
                Data = new ResultGetAllAppointmentsForExpertPanelDto()
                {
                    AppointmentForExpertPanelDtos = appointmentsForExpertPanel,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
