using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.Factors
{
    public class UpdateFactorStatusService : IUpdateFactorStatusService
    {
        private readonly ApplicationContext _context;

        public UpdateFactorStatusService(ApplicationContext context)
        {
            _context = context;
        }
        public ResultDto Execute(string factorNumber, FactorStatus factorStatus, TransactionStatus transactionStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var factor = _context.Factors
                    .Include(_ => _.Appointments)
                    .ThenInclude(_ => _.TimeOfDay)
                    .Include(f => f.ConsumerInformation)
                    .Include(f => f.ExpertInformation)
                    .First(_ => _.FactorNumber == factorNumber);

                if (factor == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = string.Empty
                    };
                }

                var financialTransaction = new FinancialTransaction()
                {
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Factor = factor,
                    FactorId = factor.Id,
                    PayerId = factor.ConsumerInformation.ConsumerId,
                    ReceiverId = factor.ExpertInformation.ExpertId,
                    Price_Digit = factor.TotalPrice,
                    Price_String = factor.TotalPrice.ToString().GetPersianNumber(),
                    Status = transactionStatus,
                    TransactionType = TransactionType.PayFromCreditCard
                };

                factor.FactorStatus = factorStatus;

                if (factorStatus == FactorStatus.SuccessfulPayment)
                {
                    foreach (var appointment in factor.Appointments)
                    {
                        appointment.TimeOfDay.IsReserved = true;
                    }
                }

                _context.FinancialTransactions.Add(financialTransaction);

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
