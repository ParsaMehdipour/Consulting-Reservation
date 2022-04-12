using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class UpdateFinancialTransactionStatusService : IUpdateFinancialTransactionStatusService
    {
        private readonly ApplicationContext _context;

        public UpdateFinancialTransactionStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string transactionNumber, TransactionStatus status)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var financialTransaction = _context.FinancialTransactions.FirstOrDefault(_ => _.TransactionNumber == transactionNumber);

                if (financialTransaction == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = string.Empty
                    };
                }

                financialTransaction.Status = status;

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
