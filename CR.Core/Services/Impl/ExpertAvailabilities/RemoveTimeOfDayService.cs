using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.ExpertAvailabilities
{
    public class RemoveTimeOfDayService : IRemoveTimeOfDayService
    {
        private readonly ApplicationContext _context;

        public RemoveTimeOfDayService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var timeOfDay = _context.TimeOfDays.FirstOrDefault(t => t.Id == id);

                if (timeOfDay == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "زمانبندی یافت نشد"
                    };
                }

                _context.TimeOfDays.Remove(timeOfDay);

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

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
