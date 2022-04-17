using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
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
                var timeOfDay = _context.TimeOfDays
                    .Include(_ => _.Appointments).FirstOrDefault(t => t.Id == id);

                if (timeOfDay == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "زمانبندی یافت نشد"
                    };
                }

                if (timeOfDay.Appointments.Any())
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "بدلیل وجود این زمان در نوبت قابلیت حذف آن وجود ندارد "
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

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
