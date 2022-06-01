using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class ChangeConsumerStatusService : IChangeConsumerStatusService
    {
        private readonly ApplicationContext _context;

        public ChangeConsumerStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long consumertId, bool activeStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var consumer = _context.Users.FirstOrDefault(u => u.Id == consumertId);

                if (consumer == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر پیدا نشد !!"
                    };
                }

                consumer.IsActive = activeStatus;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "وضعیت کاربر با موفقیت تغییر کرد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
