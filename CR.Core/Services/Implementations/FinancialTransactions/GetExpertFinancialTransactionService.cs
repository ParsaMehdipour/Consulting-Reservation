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
    public class GetExpertFinancialTransactionService : IGetExpertFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public GetExpertFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertFinancialTransactionsDto> Execute(long expertId, int Page = 1, int PageSize = 20)
        {
            var expertFinancialTransactions = _context.FinancialTransactions
                .Where(_ => _.ReceiverId == expertId)
                .OrderByDescending(_ => _.CreateDate)
                .AsNoTracking();

            var list = new List<ExpertFinancialTransactionDto>();

            foreach (var financialTransaction in expertFinancialTransactions)
            {
                var payer = GetPayer(financialTransaction.PayerId, _context);

                var expertFinancialTransaction = new ExpertFinancialTransactionDto()
                {
                    Id = financialTransaction.Id,
                    AdminFullName = payer.FirstName + " " + payer.LastName,
                    AdminIconSrc = payer.IconSrc,
                    CreateDate = financialTransaction.CreateDate_String,
                    Price = financialTransaction.Price_Digit.ToString("n0"),
                    TransactionStatus = financialTransaction.Status.GetDisplayName(),
                    TransactionType = financialTransaction.TransactionType.GetDisplayName()
                };

                list.Add(expertFinancialTransaction);
            }

            list = list.AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetExpertFinancialTransactionsDto>()
            {
                Data = new ResultGetExpertFinancialTransactionsDto()
                {
                    CurrentPage = Page,
                    ExpertFinancialTransactionDtos = list,
                    PageSize = PageSize,
                    RowCount = rowCount,
                }
            };
        }

        private User GetPayer(long payerId, ApplicationContext context)
        {
            return context.Users.Find(payerId);
        }
    }
}
