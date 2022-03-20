using CR.Common.DTOs;
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
                    .Include(f => f.ExpertInformation)
                    .First(_ => _.Id == factorId);

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
                        Message = "تراکنش یافت نشد!!",
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
                    }
                }


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
                            CallingType = _.CallingType,
                            AppointmentDate = _.TimeOfDay.Day.Date
                        }).ToList()
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
