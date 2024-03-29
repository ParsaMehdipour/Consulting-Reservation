﻿using CR.Common.Convertor;
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
    public class GetBlogsForExpertPanelService : IGetBlogsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetBlogsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetBlogsForExpertPanelDto> Execute(long userId, int page = 1, int pageSize = 20)
        {
            var blogs = _context.Blogs
                .Include(_ => _.BlogCategory)
                .Where(_ => _.UserId == userId)
                .AsNoTracking()
                .Select(_ => new BlogForExpertPanelDto()
                {
                    Id = _.Id,
                    BlogPictureSrc = _.PictureSrc ?? "assets/img/img-pharmacy1.jpg",
                    Title = (_.Title.Length > 15) ? _.Title.Substring(0, Math.Min(_.Title.Length, 15)) + "..." : _.Title,
                    BlogCategory = _.BlogCategory.Name,
                    CreateDate = _.CreateDate.ToShamsi(),
                    PublishDate = _.PublishDate.ToShamsi(),
                    Status = _.Status,
                }).AsEnumerable()
                .ToPaged(page, pageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetBlogsForExpertPanelDto>()
            {
                Data = new ResultGetBlogsForExpertPanelDto()
                {
                    BlogForExpertPanelDtos = blogs,
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowCount
                },
                IsSuccess = true
            };
        }
    }
}
