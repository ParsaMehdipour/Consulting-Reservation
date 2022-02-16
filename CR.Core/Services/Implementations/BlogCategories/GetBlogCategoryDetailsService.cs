using CR.Common.DTOs;
using CR.Core.DTOs.BlogCategories;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.BlogCategories
{
    public class GetBlogCategoryDetailsService : IGetBlogCategoryDetailsService
    {
        private readonly ApplicationContext _context;

        public GetBlogCategoryDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<BlogCategoryDetailsForAdminDto> Execute(long id)
        {
            var blogCategory = _context.BlogCategories.Find(id);

            if (blogCategory == null)
            {
                return new ResultDto<BlogCategoryDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات گروه مقاله یافت نشد!!"
                };
            }

            var blogCategoryDetailsForAdminPanel = new BlogCategoryDetailsForAdminDto()
            {
                id = blogCategory.Id,
                pictureSrc = blogCategory.PictureSrc,
                slug = blogCategory.Slug,
                name = blogCategory.Name,
                description = blogCategory.Description,
                metaDescription = blogCategory.MetaDescription,
                orderNumber = blogCategory.ShowOrder
            };

            return new ResultDto<BlogCategoryDetailsForAdminDto>()
            {
                Data = blogCategoryDetailsForAdminPanel,
                IsSuccess = true
            };
        }
    }
}
