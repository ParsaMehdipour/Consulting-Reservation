using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Comments.Blogs;
using CR.Core.DTOs.ResultDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetBlogCommentsForAdminService : IGetBlogCommentsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetBlogCommentsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllBlogCommentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20)
        {
            var blogs = _context.Blogs.AsQueryable();

            var blogComments = _context.Comments
                .Include(_ => _.User)
                .Where(_ => _.TypeId == CommentType.Blog)
                .Select(_ => new BlogCommentForAdminPanelDto()
                {
                    CommenterFullName = _.User.FirstName + " " + _.User.LastName,
                    CommenterIconSrc = _.User.IconSrc ?? "assets/img/icon-256x256.png",
                    CommenterId = _.User.Id,
                    CreateDate = _.CreateDate.ToShamsi(),
                    BlogName = blogs.FirstOrDefault(e => e.Id == _.OwnerRecordId).Title,
                    BlogId = blogs.FirstOrDefault(e => e.Id == _.OwnerRecordId).Id,
                    BlogIconSrc = blogs.FirstOrDefault(e => e.Id == _.OwnerRecordId).PictureSrc,
                    Id = _.Id,
                    Message = (_.Message.Length > 15)
                        ? _.Message.Substring(0, Math.Min(_.Message.Length, 15)) + "..."
                        : _.Message,
                    Status = _.CommentStatus.GetDisplayName(),
                    IsRead = _.IsRead
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowsCount)
                .ToList();

            return new ResultDto<ResultGetAllBlogCommentsForAdminPanelDto>()
            {
                Data = new ResultGetAllBlogCommentsForAdminPanelDto()
                {
                    BlogCommentForAdminPanelDtos = blogComments,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowsCount
                },
                IsSuccess = true
            };
        }
    }
}
