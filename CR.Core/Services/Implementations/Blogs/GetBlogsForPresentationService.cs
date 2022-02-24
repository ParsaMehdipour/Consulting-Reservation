using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.ResultDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Blogs
{
    public class GetBlogsForPresentationService : IGetBlogsForPresentationService
    {
        public User User { get; set; }
        private readonly ApplicationContext _context;

        public GetBlogsForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetBlogsForPresentationDto> Execute(string searchKey, int Page = 1, int PageSize = 20)
        {

            var blogsQuery = _context.Blogs
                .Where(_ => _.Status && _.PublishDate <= DateTime.Now.Date)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                blogsQuery = blogsQuery.Where(_ => _.Title.Contains(searchKey)
                                                   || _.Keywords.Contains(searchKey)
                                                   || _.ShortDescription.Contains(searchKey));
            }

            List<BlogForPresentationDto> blogs = blogsQuery.Select(_ => new BlogForPresentationDto
            {
                AuthorFullName = GetAuthorName(_.UserId, _context),
                AuthorIconSrc = GetAuthorIconSrc(_.UserId, _context),
                AuthorInformationId = GetAuthorInformationId(_.UserId, _context),
                PictureSrc = _.PictureSrc,
                PublishDate = _.PublishDate.ToShamsi(),
                ShortDescription = (_.ShortDescription.Length > 15) ? _.ShortDescription.Substring(0, Math.Min(_.ShortDescription.Length, 50)) + "..." : _.ShortDescription,
                Slug = _.Slug,
                Title = _.Title
            }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetBlogsForPresentationDto>()
            {
                Data = new ResultGetBlogsForPresentationDto()
                {
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                    BlogForPresentationDtos = blogs
                },
                IsSuccess = true
            };
        }

        private static string GetAuthorName(long id, ApplicationContext context)
        {
            var user = context.Users.Find(id);

            if (user != null)
                return user.FirstName + " " + user.LastName;
            return "سامانه چاله چوله";
        }

        private static string GetAuthorIconSrc(long id, ApplicationContext context)
        {
            var user = context.Users.Find(id);

            if (user != null)
                return user.IconSrc;
            return "assets/img/favicon-32x32.png";
        }

        private static long GetAuthorInformationId(long id, ApplicationContext context)
        {
            var user = context.Users.FirstOrDefault(_ => _.Id == id);

            if (user != null)
                return user.ExpertInformationId.Value;
            return 0;
        }
    }
}
