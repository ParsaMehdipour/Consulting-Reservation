using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Experts
{
    public class ChangeExpertStatusService : IChangeExpertStatusService
    {
        private readonly ApplicationContext _context;

        public ChangeExpertStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long expertId,bool activeStatus)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expert = _context.Users.FirstOrDefault(u => u.Id == expertId);

                if (expert == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "پزشک پیدا نشد !!"
                    };
                }

                expert.IsActive = activeStatus;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "وضعیت پزشک با موفقیت تغیر کرد"
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
