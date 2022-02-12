using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Impl.Appointment
{
    public class AddAppointmentService : IAddAppointmentService
    {
        private readonly ApplicationContext _context;

        public AddAppointmentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<string> Execute(List<RequestAddAppointmentDto> requests, long consumerId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                List<DataAccess.Entities.Appointments.Appointment> appointments =
                    new List<DataAccess.Entities.Appointments.Appointment>();
                var expertInformation = new ExpertInformation();
                var consumerInformation = new ConsumerInfromation();

                var factor = new Factor()
                {
                    FactorStatus = FactorStatus.Waiting,
                    FactorNumber = GetLastFactorNumber(),
                };

                _context.Factors.Add(factor);
                _context.SaveChanges();


                foreach (var request in requests)
                {
                    var timeOfDay = _context.TimeOfDays
                        .Include(t => t.ExpertInformation)
                        .ThenInclude(e => e.CommissionAndDiscount)
                        .FirstOrDefault(t => t.Id == request.timeOfDayId);

                    if (timeOfDay == null)
                    {
                        return new ResultDto<string>
                        {
                            IsSuccess = false,
                            Message = "زمانبندی یافت نشد!!",
                            Data = null
                        };
                    }

                    consumerInformation = _context.ConsumerInfromations.FirstOrDefault(c => c.ConsumerId == consumerId);

                    if (consumerInformation == null)
                    {
                        return new ResultDto<string>
                        {
                            IsSuccess = false,
                            Message = "اطلاعات شما یافت نشد!!",
                            Data = null
                        };
                    }


                    expertInformation =
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



                    var appointment = new DataAccess.Entities.Appointments.Appointment()
                    {
                        ConsumerInformation = consumerInformation,
                        ConsumerInformationId = consumerInformation.Id,
                        ExpertInformation = expertInformation,
                        ExpertInformationId = expertInformation.Id,
                        TimeOfDay = timeOfDay,
                        TimeOfDayId = timeOfDay.Id,
                        Price = CheckCallingType(request.callingType, timeOfDay),
                        CallingType = request.callingType,
                        FactorId = factor.Id,
                        Factor = factor
                    };

                    _context.Appointments.Add(appointment);
                    appointments.Add(appointment);

                    _context.SaveChanges();

                    timeOfDay.Appointment = appointment;
                    timeOfDay.AppointmentId = appointment.Id;


                    consumerInformation.ConsumerAppointments.Add(appointment);

                    expertInformation.ExpertAppointments.Add(appointment);

                }

                factor.Appointments.AddRange(appointments);
                factor.TotalPrice = appointments.Sum(a => a.Price) ?? 0;
                factor.ExpertInformationId = expertInformation.Id;
                factor.ExpertInformation = expertInformation;
                factor.ConsumerInformationId = consumerInformation.Id;
                factor.ConsumerInformation = consumerInformation;

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

                return new ResultDto<string>()
                {
                    Data = null,
                    Message = "خطا از سمت سرور!!",
                    IsSuccess = false
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private string GetLastFactorNumber()
        {
            var factor = _context.Factors.OrderBy(f => f.Id).LastOrDefault();

            if (factor == null)
            {
                return 1.ToString();
            }

            return (Convert.ToInt32(factor.FactorNumber) + 1).ToString();
        }

        private long CheckCallingType(CallingType callingType, TimeOfDay entity)
        {
            long commission = 0;
            long discount = 0;

            if (callingType == CallingType.PhoneCall)
            {
                if (entity.ExpertInformation.CommissionAndDiscount != null)
                {
                    commission = (long)((entity.ExpertInformation.CommissionAndDiscount.PhoneCallCommission * entity.PhoneCallPrice) / 100);
                    discount = (long)((entity.ExpertInformation.CommissionAndDiscount.PhoneCallDiscount * entity.PhoneCallPrice) / 100);
                    return ((entity.PhoneCallPrice + commission) - discount);
                }

                return entity.VoiceCallPrice;
            }
            if (callingType == CallingType.VoiceCall)
            {
                if (entity.ExpertInformation.CommissionAndDiscount != null)
                {
                    commission = (long)((entity.ExpertInformation.CommissionAndDiscount.VoiceCallCommission * entity.VoiceCallPrice) / 100);
                    discount = (long)((entity.ExpertInformation.CommissionAndDiscount.VoiceCallDiscount * entity.VoiceCallPrice) / 100);
                    return ((entity.VoiceCallPrice + commission) - discount);
                }
                return entity.VoiceCallPrice;
            }
            if (entity.ExpertInformation.CommissionAndDiscount != null)
            {
                commission = (long)((entity.ExpertInformation.CommissionAndDiscount.TextCallCommission * entity.TextCallPrice) / 100);
                discount = (long)((entity.ExpertInformation.CommissionAndDiscount.TextCallDiscount * entity.TextCallPrice) / 100);
                return ((entity.TextCallPrice + commission) - discount);
            }

            return entity.TextCallPrice;

        }
    }

}
