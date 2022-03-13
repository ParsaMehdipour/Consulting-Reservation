using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Comments;
using CR.DataAccess.Enums;
using System;

namespace CR.Core.Services.Implementations.Comments
{
    public class AddNewCommentService : IAddNewCommentService
    {
        private readonly ApplicationContext _context;

        public AddNewCommentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewCommentDto request, long userId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var comment = new Comment()
                {
                    UserId = userId,
                    User = _context.Users.Find(userId),
                    CommentStatus = CommentStatus.Waiting,
                    IsRead = false,
                    Message = request.message,
                    OwnerRecordId = request.ownerRecordId,
                    TypeId = request.typeId,
                };

                _context.Comments.Add(comment);

                if (request.parentId is > 0)
                {
                    var parentComment = GetParentComment(request.parentId);

                    comment.Parent = parentComment;
                    comment.ParentId = request.parentId;

                    _context.SaveChanges();

                    parentComment.Children.Add(comment);

                }

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نظر شما با موفقیت ثبت شد"
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

        private Comment GetParentComment(long? parentId)
        {
            return _context.Comments.Find(parentId);
        }
    }
}
