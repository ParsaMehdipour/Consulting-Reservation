using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Appointment
{
    public class ChangeAppointmentStatusService : IChangeAppointmentStatusService
    {
        private readonly ApplicationContext _context;

        public ChangeAppointmentStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestChangeAppointmentStatusDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Id == request.AppointmentId);

                if (appointment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نوبت پیدا نشد!!"
                    };
                }

                appointment.AppointmentStatus = request.AppointmentStatus;

                if (!string.IsNullOrWhiteSpace(request.Reason))
                {
                    appointment.Reason = request.Reason;
                }

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "وضعیت نوبت با موفقیت تغیر یافت"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
