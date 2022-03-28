using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.FinancialTransactions
{
    public class GetCheckoutFinancialTransactionsService : IGetCheckoutFinancialTransactionsService
    {
        private readonly ApplicationContext _context;

        public GetCheckoutFinancialTransactionsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetCheckoutFinancialTransactionsDto> Execute(int Page = 1, int PageSize = 20)
        {
            var financialTransactions = _context.FinancialTransactions
                .Where(_ => _.TransactionType == TransactionType.Checkout)
                .OrderByDescending(_ => _.CreateDate);

            var list = new List<CheckoutFinancialTransactionForAdminDto>();

            foreach (var financialTransaction in financialTransactions)
            {
                var receiver = _context.Users.Find(financialTransaction.ReceiverId);

                var checkoutFinancialTransaction = new CheckoutFinancialTransactionForAdminDto()
                {
                    CreateDate = financialTransaction.CreateDate_String,
                    Price = financialTransaction.Price_Digit.ToString("n0"),
                    ReceiverIconSrc = receiver.IconSrc,
                    ReceiverFullName = receiver.FirstName + " " + receiver.LastName,
                    ReceiverId = financialTransaction.ReceiverId.Value,
                    TransactionStatus = financialTransaction.TransactionType.GetDisplayName()
                };

                list.Add(checkoutFinancialTransaction);
            }

            list = list.AsEnumerable()
                .ToPaged(Page, PageSize, out var rowsCount)
                .ToList();

            return new ResultDto<ResultGetCheckoutFinancialTransactionsDto>()
            {
                Data = new ResultGetCheckoutFinancialTransactionsDto()
                {
                    CheckoutFinancialTransactionForAdminDtos = list,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowsCount
                },
                IsSuccess = true
            };
        }
    }
}
