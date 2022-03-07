using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
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
                .OrderByDescending(_ => _.CreateDate)
                .Select(_ => new FinancialTransactionForAdminDto
                {
                    CreateDate = _.CreateDate_String,
                    PayerId = _.PayerId,
                    PayerIconSrc = GetPayer(_.PayerId, _context).IconSrc,
                    PayerFullName = GetPayer(_.PayerId, _context).FirstName + " " + GetPayer(_.PayerId, _context).LastName,
                    ReceiverId = _.ReceiverId ?? 0,
                    ReceiverIconSrc = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.IconSrc : "assets/img/favicon-32x32.png",
                    ReceiverFullName = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.FirstName + " " + _.Factor.ExpertInformation.LastName : "",
                    Price = _.Price_String,
                    TransactionStatus = _.Status.GetDisplayName(),
                    TransactionType = _.TransactionType.GetDisplayName(),
                })
                .AsNoTracking()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
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

        private static User GetPayer(long payerId, ApplicationContext context)
        {
            return context.ConsumerInfromations.Include(_ => _.Consumer).FirstOrDefault(_ => _.ConsumerId == payerId)?.Consumer;
        }
    }
}
