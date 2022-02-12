﻿using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Payment;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.FinancialTransactions;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.FinancialTransactions
{
    public class AddChargeWalletFinancialTransactionService : IAddChargeWalletFinancialTransactionService
    {
        private readonly ApplicationContext _context;

        public AddChargeWalletFinancialTransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<RedirectToPaymentForWalletChargeDto> Execute(long payerId, int price)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (payerId == 0 || price == 0)
                {
                    return new ResultDto<RedirectToPaymentForWalletChargeDto>()
                    {
                        Data = new RedirectToPaymentForWalletChargeDto(),
                        IsSuccess = false,
                        Message = "لطفا مبلغ را وارد کنید"
                    };
                }

                var financialTransaction = new FinancialTransaction()
                {
                    PayerId = payerId,
                    Price_Digit = price,
                    Price_String = price.ToString().GetPersianNumber(),
                    TransactionNumber = GetLastTransactionNumber(),
                    TransactionType = TransactionType.ChargeWallet
                };

                _context.FinancialTransactions.Add(financialTransaction);

                _context.SaveChanges();

                return new ResultDto<RedirectToPaymentForWalletChargeDto>()
                {
                    Data = new RedirectToPaymentForWalletChargeDto()
                    {
                        price = financialTransaction.Price_Digit,
                        transactionNumber = Convert.ToInt32(financialTransaction.TransactionNumber)
                    },
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<RedirectToPaymentForWalletChargeDto>()
                {
                    Data = new RedirectToPaymentForWalletChargeDto(),
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private string GetLastTransactionNumber()
        {
            var financialTransactions = _context.FinancialTransactions.OrderBy(f => f.Id).LastOrDefault();

            if (financialTransactions == null)
            {
                return 1.ToString();
            }

            return (Convert.ToInt32(financialTransactions.TransactionNumber) + 1).ToString();
        }
    }
}