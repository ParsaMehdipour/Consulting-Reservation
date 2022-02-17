using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Appointment
{
    public class ChangeAppointmentStatusServiceBool : IChangeAppointmentStatusServiceBool
    {
        private readonly ApplicationContext _context;

        public ChangeAppointmentStatusServiceBool(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long appointmentId, bool activeStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

                if (appointment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نوبت پیدا نشد!!"
                    };
                }

                //appointment.IsActive = activeStatus;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "وضعیت نوبت با موفقیت تغیر کرد"
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
