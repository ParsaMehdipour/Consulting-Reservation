using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.Timings
{
    public class RemoveTimingService : IRemoveTimingService
    {
        private readonly ApplicationContext _context;

        public RemoveTimingService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var timing = _context.Timings.FirstOrDefault(t => t.Id == id);

                if (timing == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "زمانبندی یافت نشد!!"
                    };
                }

                _context.Timings.Remove(timing);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "زمانبندی با موفقیت حذف شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
