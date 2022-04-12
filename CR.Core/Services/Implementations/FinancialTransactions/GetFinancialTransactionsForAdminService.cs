using CR.Common.Convertor;
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
    public class GetFinancialTransactionsForAdminService : IGetFinancialTransactionsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetFinancialTransactionsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetFinancialTransactionsForAdminPanel> Execute(int Page = 1, int PageSize = 20)
        {
            var financialTransactions = _context.FinancialTransactions
                .Include(_ => _.Factor)
                .Include(_ => _.Factor.ExpertInformation)
                .Include(_ => _.Factor.ConsumerInformation)
                .OrderByDescending(_ => _.CreateDate).AsNoTracking();

            var list = new List<FinancialTransactionForAdminDto>();

            foreach (var financialTransaction in financialTransactions)
            {
                var payer = GetUser(financialTransaction.PayerId, _context);

                var receiver = GetUser(financialTransaction.ReceiverId, _context);

                var financialTransactionDto = new FinancialTransactionForAdminDto()
                {
                    CreateDate = financialTransaction.CreateDate.ToShamsi(),
                    PayerFullName = payer.FirstName + " " + payer.LastName,
                    PayerIconSrc = payer.IconSrc ?? "assets/img/User.png",
                    PayerId = financialTransaction.ReceiverId,
                    Price = financialTransaction.Price_Digit.ToString("n0"),
                    ReceiverFullName = receiver.FirstName + " " + receiver.LastName,
                    ReceiverIconSrc = receiver.IconSrc ?? "assets/img/User.png",
                    ReceiverId = financialTransaction.ReceiverId,
                    TransactionStatus = financialTransaction.Status.GetDisplayName(),
                    TransactionType = financialTransaction.TransactionType.GetDisplayName(),
                    PayerUserFlag = payer.UserFlag,
                    ReceiverUserFlag = receiver.UserFlag
                };

                list.Add(financialTransactionDto);

            }

            list = list.AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetFinancialTransactionsForAdminPanel>()
            {
                Data = new ResultGetFinancialTransactionsForAdminPanel()
                {
                    FinancialTransactionForAdminDtos = list,
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
