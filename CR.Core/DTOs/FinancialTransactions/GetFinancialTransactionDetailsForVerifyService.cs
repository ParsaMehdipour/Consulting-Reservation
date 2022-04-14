using CR.Common.DTOs;
using CR.Core.DTOs.Factors;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.DTOs.FinancialTransactions
{
    public class GetFinancialTransactionDetailsForVerifyService : IGetFinancialTransactionDetailsForVerifyService
    {
        private readonly ApplicationContext _context;

        public GetFinancialTransactionDetailsForVerifyService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<FactorDetailsForVerifyDto> Execute(string transactionNumber)
        {
            var factor = _context.FinancialTransactions
                .Include(_ => _.Factor)
                .ThenInclude(_ => _.ConsumerInformation)
                .FirstOrDefault(_ => _.TransactionNumber == transactionNumber)
                ?.Factor;

            if (factor == null)
            {
                return new ResultDto<FactorDetailsForVerifyDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات فاکتور یافت نشد!!"
                };
            }

            return new ResultDto<FactorDetailsForVerifyDto>()
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = new FactorDetailsForVerifyDto()
                {
                    Id = factor.Id,
                    UserId = factor.ConsumerInformation.ConsumerId,
                    price = factor.TotalPrice,
                    refId = factor.RefId
                }
            };
        }
    }
}
