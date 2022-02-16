using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.ExpertImages;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.ExpertImages
{
    public class RemoveExpertImagesService : IRemoveExpertImagesService
    {
        private readonly ApplicationContext _context;

        public RemoveExpertImagesService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                var expertImage = _context.ExpertImages.FirstOrDefault(i => i.Id == id);

                if (expertImage == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تصویر مورد نظر پیدا نشد!!"
                    };
                }

                _context.ExpertImages.Remove(expertImage);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تصویر با موفقیت حذف شد"
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
