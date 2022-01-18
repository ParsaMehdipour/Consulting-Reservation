using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
{
    public class GetAppointmentDetailsForExpertPanelService : IGetAppointmentDetailsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetAppointmentDetailsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AppointmentDetailsForExpertPanel> Execute(long id)
        {
            var appointment = _context.Appointments
                .Include(a=>a.TimeOfDay)
                .ThenInclude(t=>t.Day)
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                return new ResultDto<AppointmentDetailsForExpertPanel>()
                {
                    IsSuccess = false,
                    Message = "!!نوبت یافت نشد"
                };
            }

            return new ResultDto<AppointmentDetailsForExpertPanel>()
            {
                Data = new AppointmentDetailsForExpertPanel()
                {
                    appointmentDate = appointment.TimeOfDay.Day.Date_String,
                    appointmentStatus = appointment.AppointmentStatus.GetDisplayName(),
                    appointmentPrice = appointment.Price.ToString().GetPersianNumber(),
                    id = appointment.Id
                },
                IsSuccess = true
            };
        }
    }
}
