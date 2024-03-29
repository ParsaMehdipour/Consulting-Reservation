﻿using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.Factors
{
    public class UpdateFactorRefIdService : IUpdateFactorRefIdService
    {
        private readonly ApplicationContext _context;

        public UpdateFactorRefIdService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long factorId, string refId)
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

                factor.RefId = refId;

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
