using CR.Common.DTOs;
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
        public ResultDto Execute(long factorId, FactorStatus factorStatus, TransactionStatus transactionStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var factor = _context.Factors
                    .Include(_ => _.Appointments)
                    .ThenInclude(_ => _.TimeOfDay)
                    .Include(f => f.ConsumerInformation)
                    .Include(f => f.ExpertInformation)
                    .First(_ => _.Id == factorId);

                if (factor == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = string.Empty
                    };
                }

                var financialTransaction = _context.FinancialTransactions.FirstOrDefault(_ => _.FactorId == factorId);

                if (financialTransaction == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تراکنش یافت نشد!!"
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

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = string.Empty
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
