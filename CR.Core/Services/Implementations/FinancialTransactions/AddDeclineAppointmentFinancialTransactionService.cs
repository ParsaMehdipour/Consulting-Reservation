using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class AddDeclineAppointmentFinancialTransactionService : IAddDeclineAppointmentFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddDeclineAppointmentFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long receiverId, long appointmentId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (receiverId == 0 || appointmentId == 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "یکی از پارامتر های وروردی صحیح نسیت"
                    };
                }

                var appointment = _context.Appointments
                    .Include(_ => _.TimeOfDay)
                    .FirstOrDefault(_ => _.Id == appointmentId);

                if (appointment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نوبت یافت نشد"
                    };
                }

                appointment.AppointmentStatus = AppointmentStatus.Declined;

                var timeOfDay = _context.TimeOfDays.Find(appointment.TimeOfDayId);

                if (timeOfDay == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "روز یافت نشد"
                    };
                }

                timeOfDay.IsReserved = false;

                var factor = _context.Factors.Include(_ => _.Appointments)
                    .FirstOrDefault(_ => _.Id == appointment.FactorId);

                if (factor == null)
                {
                    return new ResultDto()
                    {
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

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();

                return new ResultDto()
                {
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
