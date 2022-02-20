using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.ResultDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Blogs
{
    public class GetBlogsForAdminPanelService : IGetBlogsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetBlogsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetBlogsForAdminPanelDto> Execute(int page = 1, int pageSize = 20)
        {
            var blogs = _context.Blogs
                .Include(_ => _.BlogCategory)
                .AsNoTracking()
                .Select(_ => new BlogForAdminDto
                {
                    Id = _.Id,
                    BlogPictureSrc = _.PictureSrc ?? "assets/img/img-pharmacy1.jpg",
                    Title = (_.Title.Length > 15) ? _.Title.Substring(0, Math.Min(_.ShortDescription.Length, 15)) + "..." : _.Title,
                    Author = GetAuthorName(_.UserId, _context),
                    BlogCategory = _.BlogCategory.Name,
                    CreateDate = _.CreateDate.ToShamsi(),
                    PublishDate = _.PublishDate.ToShamsi(),
                    Status = _.Status,
                }).AsEnumerable()
                .ToPaged(page, pageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetBlogsForAdminPanelDto>()
            {
                Data = new ResultGetBlogsForAdminPanelDto()
                {
                    BlogForAdminPanelDtos = blogs,
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowCount,
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
    }
}
