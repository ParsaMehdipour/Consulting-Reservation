using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                var comment = _context.Comments
                    .Include(_ => _.Rate)
                    .FirstOrDefault(_ => _.Id == request.id);

                if (comment == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نظر موردنظر یافت نشد!!"
                    };
                }

                comment.CommentStatus = request.status;

                if (comment.TypeId == CommentType.Expert)
                {
                    if (comment.CommentStatus == CommentStatus.Accepted && comment.Rate.FirstOrDefault()?.Rate != 0)
                    {
                        var expertInformation = _context.ExpertInformations.Find(comment.OwnerRecordId);

                        expertInformation.AverageRate = (decimal)_context.Ratings
                            .Where(r => r.Comment.TypeId == CommentType.Expert && r.Comment.OwnerRecordId == expertInformation.Id && r.Rate > 0)
                            .Average(r => r.Rate);
                    }
                }

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
