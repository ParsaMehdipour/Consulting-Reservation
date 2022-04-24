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
    public class AddDeclineAppointmentConsumerSideFinancialTransactionService : IAddDeclineAppointmentConsumerSideFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddDeclineAppointmentConsumerSideFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultDeclineAppointmentConsumerSideDto> Execute(long receiverId, long appointmentId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (receiverId == 0 || appointmentId == 0)
                {
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
                        IsSuccess = false,
                        Message = "یکی از پارامتر های وروردی صحیح نسیت"
                    };
                }

                var appointment = _context.Appointments
                    .Include(_ => _.TimeOfDay)
                    .ThenInclude(_ => _.Day)
                    .Include(_ => _.ExpertInformation)
                    .ThenInclude(_ => _.Expert)
                    .FirstOrDefault(_ => _.Id == appointmentId);

                if (appointment == null)
                {
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
                        IsSuccess = false,
                        Message = "نوبت یافت نشد"
                    };
                }

                var differenceInHours = (appointment.TimeOfDay.StartTime - DateTime.Now).TotalHours;
                var differenceInDays = (appointment.TimeOfDay.StartTime - DateTime.Now).TotalDays;

                if (differenceInDays < 0)
                {
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
                        IsSuccess = false,
                        Message = "به دلیل اینکه زمان مشاوره گذشته است، امکان لغو نوبت وجود ندارد"
                    };
                }

                if (differenceInHours < 1)
                {
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
                        IsSuccess = false,
                        Message = "به دلیل اینکه کمتر از یک ساعت تا شروع نوبت نمانده امکان لغو نوبت وجود ندارد"
                    };
                }

                appointment.AppointmentStatus = AppointmentStatus.Declined;

                var timeOfDay = _context.TimeOfDays.Find(appointment.TimeOfDayId);

                if (timeOfDay == null)
                {
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
                        IsSuccess = false,
                        Message = "روز یافت نشد"
                    };
                }

                timeOfDay.IsReserved = false;

                if (differenceInHours < 24)
                {
                    var financialTransactionWithLessPrice = new FinancialTransaction()
                    {
                        PayerId = _context.Users.FirstOrDefault(_ => _.UserFlag == UserFlag.Admin)!.Id,
                        ReceiverId = receiverId,
                        Price_Digit = appointment.Price.Value / 2,
                        CreateDate_String = DateTime.Now.ToShamsi(),
                        Price_String = (appointment.Price.Value / 2).ToString().GetPersianNumber(),
                        TransactionNumber = GetLastTransactionNumber(),
                        TransactionType = TransactionType.DeclineAppointmentTransaction,
                        Status = TransactionStatus.Successful
                    };

                    var factorLessPrice = _context.Factors.Include(_ => _.Appointments)
                        .FirstOrDefault(_ => _.Id == appointment.FactorId);

                    if (factorLessPrice == null)
                    {
                        return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                        {
                            Data = new ResultDeclineAppointmentConsumerSideDto(),
                            IsSuccess = false,
                            Message = "فاکتور یافت نشد"
                        };
                    }

                    factorLessPrice.TotalPrice = factorLessPrice.TotalPrice - appointment.Price.Value;

                    if (factorLessPrice.Appointments.Any(_ => _.AppointmentStatus == AppointmentStatus.Completed
                                                     || _.AppointmentStatus == AppointmentStatus.NotDone
                                                     || _.AppointmentStatus == AppointmentStatus.Waiting) is false)
                    {
                        factorLessPrice.FactorStatus = FactorStatus.Declined;
                    }

                    _context.FinancialTransactions.Add(financialTransactionWithLessPrice);

                    _context.SaveChanges();

                    transaction.Commit();

                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto()
                        {
                            time = appointment.TimeOfDay.StartHour + " - " + appointment.TimeOfDay.FinishHour,
                            date = appointment.TimeOfDay.Day.Date_String,
                            phoneNumber = appointment.ExpertInformation.Expert.PhoneNumber
                        },
                        IsSuccess = true,
                        Message = "نوبت با موفقیت لغو گردید و مبلغ آن با کسر جریمه به کیف پول شما واریز شد"
                    };
                }

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
                    return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                    {
                        Data = new ResultDeclineAppointmentConsumerSideDto(),
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

                return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                {
                    Data = new ResultDeclineAppointmentConsumerSideDto()
                    {
                        time = appointment.TimeOfDay.StartHour + " - " + appointment.TimeOfDay.FinishHour,
                        date = appointment.TimeOfDay.Day.Date_String,
                        phoneNumber = appointment.ExpertInformation.Expert.PhoneNumber
                    },
                    IsSuccess = true,
                    Message = "نوبت با موفقیت لغو گردید و مبلغ آن بدون کسر جریمه به کیف پول شما واریز شد"
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();

                return new ResultDto<ResultDeclineAppointmentConsumerSideDto>()
                {
                    Data = new ResultDeclineAppointmentConsumerSideDto(),
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
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
