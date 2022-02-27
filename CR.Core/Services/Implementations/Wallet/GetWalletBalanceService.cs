using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Wallet;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Wallet
{
    public class GetWalletBalanceService : IGetWalletBalanceService
    {
        private readonly ApplicationContext _context;

        public GetWalletBalanceService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<int> Execute(long payerId)
        {
            var sum = _context.FinancialTransactions
                .Where(_ => _.TransactionType == TransactionType.ChargeWallet && _.PayerId == payerId)
                .Sum(_ => _.Price_Digit);

            var minus = _context.FinancialTransactions
                .Where(_ => _.TransactionType == TransactionType.PayFromWallet && _.PayerId == payerId)
                .Sum(_ => _.Price_Digit);

            var balance = Convert.ToInt32(sum - minus);

            return new ResultDto<int>()
            {
                Data = balance,
                IsSuccess = true
            };
        }
    }
}
