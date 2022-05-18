using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs.Appointments;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class AddDeclineAppointmentExpertSideFinancialTransactionService : IAddDeclineAppointmentExpertSideFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddDeclineAppointmentExpertSideFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultDeclineAppointmentExpertSideDto> Execute(long receiverId, long appointmentId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (receiverId == 0 || appointmentId == 0)
                {
                    return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                    {
                        Data = new ResultDeclineAppointmentExpertSideDto(),
                        IsSuccess = false,
                        Message = "یکی از پارامتر های وروردی صحیح نسیت"
                    };
                }

                var appointment = _context.Appointments
                    .Include(_ => _.TimeOfDay)
                    .ThenInclude(_ => _.Day)
                    .Include(_ => _.ConsumerInformation)
                    .ThenInclude(_ => _.Consumer)
                    .FirstOrDefault(_ => _.Id == appointmentId);

                if (appointment == null)
                {
                    return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                    {
                        Data = new ResultDeclineAppointmentExpertSideDto(),
                        IsSuccess = false,
                        Message = "نوبت یافت نشد"
                    };
                }

                appointment.AppointmentStatus = AppointmentStatus.Declined;
                appointment.IsClosed = true;

                if (appointment.ChatUsers != null && appointment.ChatUsers.Count > 0)
                {
                    appointment.ChatUsers.FirstOrDefault()!.ChatStatus = ChatStatus.Closed;
                }

                var timeOfDay = _context.TimeOfDays.Find(appointment.TimeOfDayId);

                if (timeOfDay == null)
                {
                    return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                    {
                        Data = new ResultDeclineAppointmentExpertSideDto(),
                        IsSuccess = false,
                        Message = "روز یافت نشد"
                    };
                }

                timeOfDay.IsReserved = false;

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = _context.Users.FirstOrDefault(_ => _.UserFlag == UserFlag.Admin)!.Id,
                    ReceiverId = receiverId,
                    Price_Digit = appointment.Price.Value,
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Price_String = appointment.Price.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    TransactionType = TransactionType.DeclineAppointmentTransaction,
                    Status = TransactionStatus.Successful
                };

                var factor = _context.Factors.Include(_ => _.Appointments)
                    .FirstOrDefault(_ => _.Id == appointment.FactorId);

                if (factor == null)
                {
                    return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                    {
                        Data = new ResultDeclineAppointmentExpertSideDto(),
                        IsSuccess = false,
                        Message = "فاکتور یافت نشد"
                    };
                }

                factor.TotalPrice = factor.TotalPrice - appointment.Price.Value;

                if (factor.Appointments.Any(_ => _.AppointmentStatus == AppointmentStatus.Completed
                                                 || _.AppointmentStatus == AppointmentStatus.NotDone
                                                 || _.AppointmentStatus == AppointmentStatus.Waiting) is false)
                {
                    factor.FactorStatus = FactorStatus.Declined;
                }

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                {
                    Data = new ResultDeclineAppointmentExpertSideDto()
                    {
                        time = appointment.TimeOfDay.StartHour + " - " + appointment.TimeOfDay.FinishHour,
                        date = appointment.TimeOfDay.Day.Date_String,
                        phoneNumber = appointment.ConsumerInformation.Consumer.PhoneNumber
                    },
                    IsSuccess = true,
                    Message = "نوبت با موفقیت لغو گردید و مبلغ آن به کیف پول کاربر واریز شد"
                };

            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<ResultDeclineAppointmentExpertSideDto>()
                {
                    Data = new ResultDeclineAppointmentExpertSideDto(),
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private string GetLastTransactionNumber()
        {
            var financialTransactions = _context.FinancialTransactions.OrderBy(f => f.Id).LastOrDefault();

            if (financialTransactions == null)
            {
                return "1";
            }


            return (Convert.ToInt32(financialTransactions.TransactionNumber) + 1).ToString();
        }
    }
}
