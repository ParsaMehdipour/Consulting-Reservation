using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Appointment
{
    public class GetAppointmentDetailsForConsumerPanelService : IGetAppointmentDetailsForConsumerPanelService
    {
        private readonly ApplicationContext _context;

        public GetAppointmentDetailsForConsumerPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AppointmentDetailsForExpertPanel> Execute(long id)
        {
            var appointment = _context.Appointments
               .Include(a => a.TimeOfDay)
               .ThenInclude(t => t.Day)
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
                    appointmentTime = appointment.TimeOfDay.StartHour + " - " + appointment.TimeOfDay.FinishHour,
                    appointmentStatus = appointment.AppointmentStatus.GetDisplayName(),
                    appointmentPrice = (appointment.RawPrice + appointment.CommissionPrice - appointment.DiscountPrice).ToString("n0"),
                    id = appointment.Id,
                    appointmentType = appointment.CallingType.GetDisplayName()
                },
                IsSuccess = true
            };
        }
    }
}
