﻿using CR.Common.DTOs;
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

        public ResultDto<int> Execute(long userId)
        {
            var sum = _context.FinancialTransactions
                .Where(_ => _.ReceiverId == userId && (_.TransactionType == TransactionType.ChargeWallet
                                                        || _.TransactionType == TransactionType.DeclineFactorTransaction
                                                        || _.TransactionType == TransactionType.DeclineAppointmentTransaction)
                                                    && _.Status == TransactionStatus.Successful).Sum(_ => _.Price_Digit);

            var minus = _context.FinancialTransactions
                .Where(_ => _.PayerId == userId && _.TransactionType == TransactionType.PayFromWallet && _.Status == TransactionStatus.Successful)
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
