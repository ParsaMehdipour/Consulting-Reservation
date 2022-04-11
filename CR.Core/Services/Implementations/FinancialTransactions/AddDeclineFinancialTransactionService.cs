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
    public class AddDeclineFinancialTransactionService : IAddDeclineFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddDeclineFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long payerId, long factorId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (payerId == 0 || factorId == 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "یکی از پارامتر های وروردی صحیح نسیت"
                    };
                }

                var factor = _context.Factors
                    .Include(_ => _.Appointments)
                    .ThenInclude(_ => _.TimeOfDay)
                    .FirstOrDefault(_ => _.Id == factorId);

                if (factor == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "فاکتور یافت نشد!!"
                    };
                }

                factor.FactorStatus = FactorStatus.Declined;

                foreach (var appointment in factor.Appointments)
                {
                    appointment.TimeOfDay.IsReserved = false;
                }

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = payerId,
                    Price_Digit = factor.TotalPrice,
                    CreateDate_String = DateTime.Now.ToShamsi(),
                    Price_String = factor.TotalPrice.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    TransactionType = TransactionType.DeclineTransaction,
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
            catch (Exception)
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
