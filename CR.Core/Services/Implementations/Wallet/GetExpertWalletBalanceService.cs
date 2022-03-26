using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Wallet;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Wallet
{
    public class GetExpertWalletBalanceService : IGetExpertWalletBalanceService
    {
        private readonly ApplicationContext _context;

        public GetExpertWalletBalanceService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<int> Execute(long receiverId)
        {
            var sum = _context.FinancialTransactions
                .Where(_ => _.ReceiverId == receiverId && _.TransactionType == TransactionType.ChargeExpertWallet && _.Status == TransactionStatus.Successful)
                .Sum(_ => _.Price_Digit);

            var minus = _context.FinancialTransactions
                .Where(_ => _.ReceiverId == receiverId && _.TransactionType == TransactionType.Checkout && _.Status == TransactionStatus.Successful)
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
