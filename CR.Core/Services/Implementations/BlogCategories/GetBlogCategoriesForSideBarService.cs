using CR.Common.DTOs;
using CR.Core.DTOs.BlogCategories;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.BlogCategories
{
    public class GetBlogCategoriesForSideBarService : IGetBlogCategoriesForSideBarService
    {
        private readonly ApplicationContext _context;

        public GetBlogCategoriesForSideBarService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<BlogCategoryForSideBarDto>> Execute()
        {
            var blogCategories = _context.BlogCategories
                .Include(c => c.Blogs)
                .Where(c => c.ParentCategory != null)
                .OrderBy(c => c.ShowOrder)
                .Take(10)
                .Select(c => new BlogCategoryForSideBarDto
                {
                    BlogsCount = c.Blogs.Count,
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            return new ResultDto<List<BlogCategoryForSideBarDto>>()
            {
                Data = blogCategories,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
