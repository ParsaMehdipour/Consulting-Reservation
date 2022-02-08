using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.ResultDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.Blogs
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
            int rowCount = 0;

            var blogs = _context.Blogs
                .Include(_ => _.BlogCategory)
                .Select(_ => new BlogForAdminDto
                {
                    Id = _.Id,
                    Title = _.Title,
                    Author = "سامانه چاله چوله",
                    BlogCategory = _.BlogCategory.Name,
                    CanonicalAddress = _.CanonicalAddress,
                    CreateDate = _.CreateDate.ToShamsi(),
                    ShortDescription = _.ShortDescription.Substring(0, Math.Min(_.ShortDescription.Length, 50)) + " ...",
                    PublishDate = _.PublishDate.ToShamsi(),
                    Status = _.Status,
                }).ToList();

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
    }
}
