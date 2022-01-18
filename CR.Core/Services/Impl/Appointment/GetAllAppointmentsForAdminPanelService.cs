using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
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
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .Include(a => a.TimeOfDay)
                .ThenInclude(d => d.Day)
                .OrderByDescending(a=>a.TimeOfDay.Day.Date)
                .Select(a => new AppointmentForAdminDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    //AppointmentPrice = a.TimeOfDay.Price.ToString().GetPersianNumber(),
                    Speciality = a.ExpertInformation.Specialty.Name,
                    ConsumerFullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ExpertIconSrc = a.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = a.Id
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
