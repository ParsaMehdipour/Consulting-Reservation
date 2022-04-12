using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class GetConsumerFinancialTransactionsService : IGetConsumerFinancialTransactionsService
    {
        private readonly ApplicationContext _context;

        public GetConsumerFinancialTransactionsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumerFinancialTransactions> Execute(long consumerId, int Page = 1, int PageSize = 20)
        {
            var financialTransactions = _context.FinancialTransactions
                .Where(_ => _.PayerId == consumerId || _.ReceiverId == consumerId)
                .OrderByDescending(_ => _.CreateDate)
                .AsNoTracking();

            var list = new List<ConsumerFinancialTransactionDto>();

            foreach (var financialTransaction in financialTransactions)
            {

                var payer = GetUser(financialTransaction.PayerId, _context);

                var receiver = GetUser(financialTransaction.ReceiverId, _context);

                var consumerFinancialTransaction = new ConsumerFinancialTransactionDto()
                {
                    CreateDate = financialTransaction.CreateDate_String,
                    PayerFullName = payer.FirstName + " " + payer.LastName,
                    payerIconSrc = payer.IconSrc,
                    PayerId = financialTransaction.PayerId,
                    ReceiverFullName = receiver.FirstName + " " + receiver.LastName,
                    ReceiverIconSrc = receiver.IconSrc,
                    ReceiverId = financialTransaction.ReceiverId,
                    Price = financialTransaction.Price_Digit.ToString("n0"),
                    TransactionStatus = financialTransaction.Status.GetDisplayName(),
                    TransactionType = financialTransaction.TransactionType.GetDisplayName()
                };

                list.Add(consumerFinancialTransaction);
            }

            list = list.AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetConsumerFinancialTransactions>()
            {
                Data = new ResultGetConsumerFinancialTransactions()
                {
                    ConsumerFinancialTransactionDtos = list,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }

        private static User GetUser(long payerId, ApplicationContext context)
        {
            return context.Users.Find(payerId);
        }

    }
}

