using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Blogs
{
    public class GetLatestBlogsForSiteService : IGetLatestBlogsForSiteService
    {
        private readonly ApplicationContext _context;

        public GetLatestBlogsForSiteService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<BlogForPresentationDto>> Execute()
        {
            var latestBlogs = _context.Blogs.Where(_ => _.PublishDate.Date <= DateTime.Now.Date && _.Status == true)
                .OrderByDescending(_ => _.PublishDate)
                .ThenBy(_ => _.ShowOrder)
                .Take(4)
                .Select(_ => new BlogForPresentationDto()
                {
                    AuthorFullName = GetAuthorName(_.UserId, _context),
                    AuthorIconSrc = GetAuthorIconSrc(_.UserId, _context),
                    AuthorInformationId = GetAuthorInformationId(_.UserId, _context),
                    PictureSrc = _.PictureSrc,
                    PublishDate = _.PublishDate.ToShamsi(),
                    ShortDescription = (_.ShortDescription.Length > 15) ? _.ShortDescription.Substring(0, Math.Min(_.ShortDescription.Length, 50)) + "..." : _.ShortDescription,
                    Slug = _.Slug,
                    Title = _.Title

                }).ToList();

            return new ResultDto<List<BlogForPresentationDto>>()
            {
                Data = latestBlogs,
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
