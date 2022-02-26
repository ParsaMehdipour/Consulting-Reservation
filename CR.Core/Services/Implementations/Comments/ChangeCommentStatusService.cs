using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.Comments
{
    public class ChangeCommentStatusService : IChangeCommentStatusService
    {
        private readonly ApplicationContext _context;

        public ChangeCommentStatusService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestChangeCommentStatusDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var comment = _context.Comments.Find(request.id);

                if (comment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نظر موردنظر یافت نشد!!"
                    };
                }

                comment.CommentStatus = request.status;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "وضعیت نظر با موفقیت تغیر کرد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
