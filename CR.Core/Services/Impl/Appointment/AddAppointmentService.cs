using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.Appointment
{
    public class AddAppointmentService : IAddAppointmentService
    {
        private readonly ApplicationContext _context;

        public AddAppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddAppointmentDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var timeOfDay = _context.TimeOfDays.FirstOrDefault(t => t.Id == request.timeOfDayId);

                if (timeOfDay == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "زمانبندی یافت نشد!!"
                    };
                }

                var consumerInformation =
                    _context.ConsumerInfromations.FirstOrDefault(c => c.ConsumerId == request.consumerId);

                if (consumerInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات شما یافت نشد!!"
                    };
                }

                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.Id == request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات متخصص یافت نشد!!"
                    };
                }

                var appointment = new DataAccess.Entities.Appointments.Appointment()
                {
                    ConsumerInformation = consumerInformation,
                    ConsumerInformationId = consumerInformation.Id,
                    ExpertInformation = expertInformation,
                    ExpertInformationId = expertInformation.Id,
                    TimeOfDay = timeOfDay,
                    TimeOfDayId = timeOfDay.Id,
                    Price = timeOfDay.Price ?? 0,
                    //IsActive = false
                };

                _context.Appointments.Add(appointment);

                _context.SaveChanges();

                timeOfDay.Appointment = appointment;
                timeOfDay.AppointmentId = appointment.Id;
                timeOfDay.IsReserved = true;

                consumerInformation.ConsumerAppointments.Add(appointment);

                expertInformation.ExpertAppointments.Add(appointment);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نوبت شما با موفقیت رزرو شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
        }
    }
}
