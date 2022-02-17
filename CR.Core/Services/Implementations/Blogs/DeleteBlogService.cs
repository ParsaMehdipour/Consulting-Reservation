using System;
using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Blogs
{
    public class DeleteBlogService : IDeleteBlogService
    {
        private readonly ApplicationContext _context;

        public DeleteBlogService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long id)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                var blog = _context.Blogs.Find(id);

                if (blog == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مقاله یافت نشد!!"
                    };
                }

                _context.Blogs.Remove(blog);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مقاله با موفقیت حذف شد"
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
        }
    }
}
