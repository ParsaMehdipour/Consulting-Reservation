using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Blogs
{
    public class GetBlogDetailsForAdminPanelService : IGetBlogDetailsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetBlogDetailsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<BlogDetailsForAdminDto> Execute(long id)
        {
            var blogDetails = _context.Blogs.Find(id);

            if (blogDetails == null)
            {
                return new ResultDto<BlogDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "مقاله یافت نشد!!"
                };
            }

            var blogDetailsForAdminPanel = new BlogDetailsForAdminDto
            {
                blogCategoryId = blogDetails.BlogCategoryId,
                canonicalAddress = blogDetails.CanonicalAddress,
                description = blogDetails.Description,
                keyWords = blogDetails.Keywords,
                metaDescription = blogDetails.MetaDescription,
                orderNumber = blogDetails.ShowOrder,
                pictureSrc = blogDetails.PictureSrc,
                publishDate = blogDetails.PublishDate,
                shortDescription = blogDetails.ShortDescription,
                slug = blogDetails.Slug,
            };

            return new ResultDto<BlogDetailsForAdminDto>()
            {
                Data = blogDetailsForAdminPanel,
                IsSuccess = true,
            };

        }
    }
}
