using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

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
            int rowCount = 0;

            var financialTransactions = _context.FinancialTransactions
                .Include(_ => _.Factor)
                .Include(_ => _.Factor.ExpertInformation)
                .Include(_ => _.Factor.ConsumerInformation)
                .OrderByDescending(_ => _.CreateDate)
                .Select(_ => new FinancialTransactionForAdminDto
                {
                    CreateDate = _.CreateDate_String,
                    PayerId = _.PayerId,
                    PayerIconSrc = _.Factor.ConsumerInformation.IconSrc,
                    PayerFullName = _.Factor.ConsumerInformation.FirstName + " " + _.Factor.ConsumerInformation.LastName,
                    ReceiverId = _.ReceiverId ?? 0,
                    ReceiverIconSrc = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.IconSrc : "/assets/img/favicon-32x32.png",
                    ReceiverFullName = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.FirstName + " " + _.Factor.ExpertInformation.LastName : "",
                    Price = _.Price_String,
                    TransactionStatus = _.Status.GetDisplayName(),
                    TransactionType = _.TransactionType.GetDisplayName(),
                })
                .AsNoTracking()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetFinancialTransactionsForAdminPanel>()
            {
                Data = new ResultGetFinancialTransactionsForAdminPanel()
                {
                    FinancialTransactionForAdminDtos = financialTransactions,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
