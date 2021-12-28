using System;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Impl.Specialites
{
    public class RemoveSpecialityService : IRemoveSpecialityService
    {
        private readonly ApplicationContext _context;

        public RemoveSpecialityService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var speciality = _context.Specialties.FirstOrDefault(s => s.Id == id);

                if (speciality == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "تخصص پیدا نشد !!"
                    };
                }

                _context.Remove(speciality);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "تخصص با موفقیت حذف شد"
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
