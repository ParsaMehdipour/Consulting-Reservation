using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetBlogCommentsForBlogDetailsByIdService : IGetBlogCommentsForBlogDetailsByIdService
    {
        private readonly ApplicationContext _context;

        public GetBlogCommentsForBlogDetailsByIdService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<BlogCommentDto>> Execute(long id)
        {
            var blog = _context.Blogs.Find(id);

            if (blog == null)
            {
                return new ResultDto<List<BlogCommentDto>>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }

            var blogComments = _context.Comments
                .Where(_ => _.TypeId == CommentType.Blog
                            && _.OwnerRecordId == blog.Id
                            && _.CommentStatus == CommentStatus.Accepted
                            && _.ParentId == null)
                .Select(_ => new BlogCommentDto()
                {
                    CommenterFullName = (_.User.FirstName != null) ? _.User.FirstName + " " + _.User.LastName : "بی نام",
                    CommenterIconSrc = _.User.IconSrc ?? "assets/img/icon-256x256.png",
                    CreateDate = _.CreateDate.ToShamsi(),
                    Id = _.Id,
                    Message = _.Message,
                    HasChildren = _.Children.Any(c => c.CommentStatus == CommentStatus.Accepted),
                    Children = _.Children.Where(c => c.CommentStatus == CommentStatus.Accepted).Select(c => new BlogCommentDto()
                    {
                        ParentId = c.ParentId.Value,
                        CommenterFullName = c.User.FirstName + " " + c.User.LastName,
                        CommenterIconSrc = c.User.IconSrc ?? "assets/img/icon-256x256.png",
                        CreateDate = c.CreateDate.ToShamsi(),
                        Id = c.Id,
                        Message = c.Message
                    }).ToList()
                }).ToList();

            return new ResultDto<List<BlogCommentDto>>()
            {
                Data = blogComments,
                IsSuccess = true
            };
        }
    }
}
