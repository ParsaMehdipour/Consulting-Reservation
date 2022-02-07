using CR.Core.DTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Impl.Blogs
{
    public class GetBlogCategoriesForDropdownService : IGetBlogCategoriesForDropdownService
    {
        private readonly ApplicationContext _context;

        public GetBlogCategoriesForDropdownService(ApplicationContext context)
        {
            _context = context;
        }

        public List<BlogCategoryForDropdownDto> Execute()
        {
            var categories = _context.BlogCategories
                .Include(c => c.ParentCategory)
                .Where(p => p.ParentCategoryId != null)
                .OrderBy(c => c.ShowOrder)
                .Select(c => new BlogCategoryForDropdownDto()
                {
                    Id = c.Id,
                    Name = $"{c.ParentCategory.Name} - {c.Name}",
                }).ToList();

            return categories;
        }
    }
}
