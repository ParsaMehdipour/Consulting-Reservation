﻿using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.Factors
{
    public class UpdateFactorSaleReferenceIdService : IUpdateFactorSaleReferenceIdService
    {
        private readonly ApplicationContext _context;

        public UpdateFactorSaleReferenceIdService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long factorId, long saleReferenceId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var factor = _context.Factors.Find(factorId);

                if (factor == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "فاکتور یافت نشد!!"
                    };
                }

                factor.SaleReferenceId = saleReferenceId;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
