﻿using CR.Common.DTOs;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs.Factors;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Factors
{
    public class UpdateFactorStatusService : IUpdateFactorStatusService
    {
        private readonly ApplicationContext _context;

        public UpdateFactorStatusService(ApplicationContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUpdateFactorStatusDto> Execute(long factorId, FactorStatus factorStatus, TransactionStatus transactionStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var factor = _context.Factors
                    .Include(_ => _.Appointments)
                    .ThenInclude(_ => _.TimeOfDay)
                    .ThenInclude(f => f.Day)
                    .Include(f => f.ConsumerInformation)
                    .ThenInclude(_ => _.Consumer)
                    .Include(f => f.ExpertInformation)
                    .ThenInclude(_ => _.Expert)
                    .FirstOrDefault(_ => _.Id == factorId);

                if (factor == null)
                {
                    return new ResultDto<ResultUpdateFactorStatusDto>()
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                        Data = new ResultUpdateFactorStatusDto()
                    };
                }

                var financialTransaction = _context.FinancialTransactions.FirstOrDefault(_ => _.FactorId == factorId);

                if (financialTransaction == null)
                {
                    return new ResultDto<ResultUpdateFactorStatusDto>()
                    {
                        IsSuccess = false,
                        Message = "تراکنش یافت نشد",
                        Data = new ResultUpdateFactorStatusDto()
                    };
                }

                financialTransaction.Status = transactionStatus;

                factor.FactorStatus = factorStatus;

                if (factorStatus == FactorStatus.SuccessfulPayment)
                {
                    foreach (var appointment in factor.Appointments)
                    {
                        appointment.TimeOfDay.IsReserved = true;
                        appointment.AppointmentStatus = AppointmentStatus.Waiting;

                        _context.SaveChanges();
                    }
                }

                var consumerAppointmentsDetailsForSMS = factor.Appointments.Select(_ => new AppointmentDetailsForSMSDto()
                {
                    Date = _.TimeOfDay.Day.Date_String,
                    Time = _.TimeOfDay.StartHour + " - " + _.TimeOfDay.FinishHour,
                    UserName = factor.ConsumerInformation.Consumer.FirstName + " " + factor.ConsumerInformation.Consumer.LastName
                }).ToList();

                var expertAppointmentsDetailsForSMS = factor.Appointments.Select(_ => new AppointmentDetailsForSMSDto()
                {
                    Date = _.TimeOfDay.Day.Date_String,
                    Time = _.TimeOfDay.StartHour + " - " + _.TimeOfDay.FinishHour,
                    UserName = factor.ExpertInformation.Expert.FirstName + " " + factor.ExpertInformation.Expert.LastName
                }).ToList();

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<ResultUpdateFactorStatusDto>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = new ResultUpdateFactorStatusDto()
                    {
                        ConsumerId = factor.ConsumerInformation.ConsumerId,
                        ExpertInformationId = factor.ExpertInformation.Id,
                        IsChat = factor.Appointments.Any(_ => _.CallingType == CallingType.TextCall || _.CallingType == CallingType.VoiceCall),
                        chatAppointments = factor.Appointments.Where(_ => _.CallingType == CallingType.TextCall || _.CallingType == CallingType.VoiceCall).Select(_ => new ChatAppointmentDto()
                        {
                            Id = _.Id,
                            CallingType = _.CallingType,
                            AppointmentDate = _.TimeOfDay.Day.Date
                        }).ToList(),
                        AppointmentDetailsForConsumerSmsDtos = consumerAppointmentsDetailsForSMS,
                        AppointmentDetailsForExpertSmsDtos = expertAppointmentsDetailsForSMS,
                        ConsumerPhoneNum = factor.ConsumerInformation.Consumer.PhoneNumber,
                        ExpertPhoneNum = factor.ExpertInformation.Expert.PhoneNumber,
                    }
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<ResultUpdateFactorStatusDto>()
                {
                    IsSuccess = false,
                    Message = string.Empty,
                    Data = new ResultUpdateFactorStatusDto()
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
