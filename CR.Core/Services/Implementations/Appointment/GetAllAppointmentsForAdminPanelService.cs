using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Appointment
{
    public class GetAllAppointmentsForAdminPanelService : IGetAllAppointmentsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllAppointmentsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllAppointmentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            var appointments = _context.Appointments
                .Include(a => a.Factor)
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .Include(a => a.TimeOfDay)
                .ThenInclude(d => d.Day)
                .OrderByDescending(a => a.TimeOfDay.Day.Date)
                .Select(a => new AppointmentForAdminDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentPrice = a.Price.Value.ToString(),
                    FactorStatus = a.Factor.FactorStatus.GetDisplayName(),
                    FactorId = a.FactorId.Value,
                    ConsumerId = a.ConsumerInformation.ConsumerId,
                    Speciality = a.ExpertInformation.Specialty.Name,
                    ConsumerFullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    ExpertId = a.ExpertInformation.ExpertId,
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ExpertIconSrc = a.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = a.Id,
                    AppointmentTime = a.TimeOfDay.StartHour + "-" + a.TimeOfDay.FinishHour
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount).ToList();

            return new ResultDto<ResultGetAllAppointmentsForAdminPanelDto>()
            {
                Data = new ResultGetAllAppointmentsForAdminPanelDto()
                {
                    AppointmentForAdminDtos = appointments,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
