using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointment;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
{
    public class GetAppointmentsForAdminDashboardService : IGetAppointmentsForAdminDashboardService
    {
        private readonly ApplicationContext _context;

        public GetAppointmentsForAdminDashboardService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<AppointmentForAdminDto>> Execute()
        {
            var appointments = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .Include(a => a.TimeOfDay)
                .ThenInclude(d => d.Day)
                .Select(a => new AppointmentForAdminDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentTime = (a.TimeOfDay.StartDate.Hour.ToString().GetPersianNumber() + ":" +
                                       a.TimeOfDay.StartDate.Minute.ToString().GetPersianNumber()) +
                                      " - " + (a.TimeOfDay.FinishDate.Hour.ToString().GetPersianNumber() + ":" +
                                               a.TimeOfDay.FinishDate.Minute.ToString().GetPersianNumber()),
                    //AppointmentPrice = a.TimeOfDay.Price.ToString().GetPersianNumber(),
                    Speciality = a.ExpertInformation.Specialty.Name,
                    ConsumerFullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ExpertIconSrc = a.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    Id = a.Id
                }).OrderByDescending(a=>a.AppointmentDate).Take(10).ToList();


            return new ResultDto<List<AppointmentForAdminDto>>()
            {
                Data = appointments,
                IsSuccess = true
            };
        }
    }
}
