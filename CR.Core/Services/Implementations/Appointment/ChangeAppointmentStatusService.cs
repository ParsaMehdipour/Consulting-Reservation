using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Appointment
{
    public class ChangeAppointmentStatusService : IChangeAppointmentStatusService
    {
        private readonly ApplicationContext _context;

        public ChangeAppointmentStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultChangeAppointmentStatusDto> Execute(RequestChangeAppointmentStatusDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var appointment = _context.Appointments
                    .Include(_ => _.ChatUsers)
                    .Include(_ => _.ConsumerInformation)
                    .ThenInclude(_ => _.Consumer)
                    .Include(a => a.ExpertInformation)
                    .Include(a => a.TimeOfDay)
                    .ThenInclude(_ => _.Day)
                    .FirstOrDefault(a => a.Id == request.AppointmentId);

                if (appointment == null)
                {
                    return new ResultDto<ResultChangeAppointmentStatusDto>()
                    {
                        Data = new ResultChangeAppointmentStatusDto(),
                        IsSuccess = false,
                        Message = "نوبت پیدا نشد!!"
                    };
                }

                if (appointment.TimeOfDay.FinishTime > DateTime.Now)
                {
                    return new ResultDto<ResultChangeAppointmentStatusDto>()
                    {
                        Data = new ResultChangeAppointmentStatusDto(),
                        IsSuccess = false,
                        Message = "به دلیل عدم اتمام زمان یا نرسیدن زمان نوبت قابلیت تغیر وضعیت برای شما امکان پذیر نیست"
                    };
                }

                appointment.AppointmentStatus = request.AppointmentStatus;

                if (appointment.ChatUsers.Count > 0)
                {
                    appointment.ChatUsers.FirstOrDefault()!.ChatStatus = ChatStatus.Ended;
                }

                if (!string.IsNullOrWhiteSpace(request.Reason))
                {
                    appointment.Reason = request.Reason;
                }

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<ResultChangeAppointmentStatusDto>()
                {
                    Data = new ResultChangeAppointmentStatusDto()
                    {
                        price = (int)appointment.RawPrice,
                        receiverId = appointment.ExpertInformation.ExpertId,
                        date = appointment.TimeOfDay.Day.Date_String,
                        time = appointment.TimeOfDay.StartHour + " - " + appointment.TimeOfDay.FinishHour,
                        phoneNumber = appointment.ConsumerInformation.Consumer.PhoneNumber,
                        appointmentStatus = request.AppointmentStatus
                    },

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
