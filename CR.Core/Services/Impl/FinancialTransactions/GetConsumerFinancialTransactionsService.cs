using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.FinancialTransactions;
using CR.Core.DTOs.ResultDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Impl.FinancialTransactions
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
            var consumerFinancialTransactions = _context.FinancialTransactions
                .Include(_ => _.Factor)
                .Include(_ => _.Factor.ExpertInformation)
                .Include(_ => _.Factor.ConsumerInformation)
                .OrderByDescending(_ => _.CreateDate)
                .Where(_ => _.PayerId == consumerId)
                .Select(_ => new ConsumerFinancialTransactionDto
                {
                    CreateDate = _.CreateDate_String,
                    ExpertInformationId = _.Factor.ExpertInformationId,
                    ExpertIconSrc = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.IconSrc : "assets/img/favicon-32x32.png",
                    ExpertFullName = (_.Factor.ExpertInformation != null) ? _.Factor.ExpertInformation.FirstName + " " + _.Factor.ExpertInformation.LastName : "",
                    Price = _.Price_String,
                    TransactionStatus = _.Status.GetDisplayName(),
                    TransactionType = _.TransactionType.GetDisplayName(),
                })
                .AsNoTracking()
                .AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetConsumerFinancialTransactions>()
            {
                Data = new ResultGetConsumerFinancialTransactions()
                {
                    ConsumerFinancialTransactionDtos = consumerFinancialTransactions,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}

