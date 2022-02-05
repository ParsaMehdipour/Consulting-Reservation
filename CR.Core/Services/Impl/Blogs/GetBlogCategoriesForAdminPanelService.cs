using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Impl.Blogs
{
    public class GetBlogCategoriesForAdminPanelService : IGetBlogCategoriesForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetBlogCategoriesForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetBlogCategoriesForAdminPanelDto> Execute(long? parentId, int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            var blogCategories = _context.BlogCategories
                .Include(b => b.ParentCategory)
                .Include(b => b.SubCategories)
                .Where(b => b.ParentCategoryId == parentId)
                .Select(b => new BlogCategoryForAdminPanelDto
                {
                    Id = b.Id,
                    PictureSrc = b.PictureSrc,
                    BlogsCount = 0,
                    CreationDate = b.CreateDate.ToShamsi(),
                    Description = b.Description,
                    Name = b.Name,
                    ShowOrder = b.ShowOrder,
                    Slug = b.Slug,
                    ParentCategory = b.ParentCategory != null ? new ParentBlogCategoryDto
                    {
                        Id = b.ParentCategory.Id,
                        Name = b.ParentCategory.Name,

                    } : null,
                    HasChild = b.SubCategories.Any()
                }).OrderBy(b => b.ShowOrder).ToList();

            return new ResultDto<ResultGetBlogCategoriesForAdminPanelDto>()
            {
                Data = new ResultGetBlogCategoriesForAdminPanelDto()
                {
                    BlogCategoryForAdminPanelDtos = blogCategories,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
