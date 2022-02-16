using System;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.BlogCategories
{
    public class DeleteBlogCategoryService : IDeleteBlogCategoryService
    {
        private readonly ApplicationContext _context;

        public DeleteBlogCategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var blogCategory = _context.BlogCategories
                    .Include(b => b.SubCategories)
                    .FirstOrDefault(b => b.Id == id);

                if (blogCategory == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "گروه مقاله یافت نشد!!"
                    };
                }

                if (blogCategory.SubCategories.Any())
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ابتدا فرزندان این گروه را حذف کنید"
                    };
                }

                _context.BlogCategories.Remove(blogCategory);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "گروه با موفقیت حذف شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
