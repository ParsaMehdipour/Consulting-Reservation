using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Impl.Appointment
{
    public class AddAppointmentService : IAddAppointmentService
    {
        private readonly ApplicationContext _context;

        public AddAppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<string> Execute(RequestAddAppointmentDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var timeOfDay = _context.TimeOfDays.FirstOrDefault(t => t.Id == request.timeOfDayId);

                if (timeOfDay == null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        Message = "زمانبندی یافت نشد!!",
                        Data = null
                    };
                }

                var consumerInformation =
                    _context.ConsumerInfromations.FirstOrDefault(c => c.ConsumerId == request.consumerId);

                if (consumerInformation == null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        Message = "اطلاعات شما یافت نشد!!",
                        Data = null
                    };
                }

                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.Id == request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        Message = "اطلاعات متخصص یافت نشد!!",
                        Data = null
                    };
                }

                var factor = new Factor()
                {
                    FactorStatus = FactorStatus.Waiting,
                    FactorNumber = GetLastFactorNumber(),
                };

                _context.Factors.Add(factor);
                _context.SaveChanges();

                var appointment = new DataAccess.Entities.Appointments.Appointment()
                {
                    ConsumerInformation = consumerInformation,
                    ConsumerInformationId = consumerInformation.Id,
                    ExpertInformation = expertInformation,
                    ExpertInformationId = expertInformation.Id,
                    TimeOfDay = timeOfDay,
                    TimeOfDayId = timeOfDay.Id,
                    //Price = timeOfDay.Price ?? 0,
                    FactorId = factor.Id,
                    Factor = factor
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

                return new ResultDto<string>
                {
                    IsSuccess = true,
                    Message = "نوبت شما با موفقیت رزرو شد",
                    Data = factor.FactorNumber
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
        }

        private string GetLastFactorNumber()
        {
            var factor = _context.Factors.OrderBy(f=>f.Id).LastOrDefault();

            if (factor == null)
            {
                return 1.ToString();
            }

            return (Convert.ToInt32(factor.FactorNumber) + 1).ToString();
        }
    }
}
