using CR.Common.DTOs;
using CR.Core.DTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                id = blogDetails.Id,
                author = GetAuthorName(blogDetails.UserId),
                authorIconSrc = GetAuthorIconSrc(blogDetails.UserId),
                authorDescription = GetAuthorDescription(blogDetails.UserId),
                title = blogDetails.Title,
                blogCategoryId = blogDetails.BlogCategoryId,
                blogCategoryName = GetBlogCategoryName(blogDetails.BlogCategoryId),
                canonicalAddress = blogDetails.CanonicalAddress,
                description = blogDetails.Description,
                keyWords = blogDetails.Keywords,
                metaDescription = blogDetails.MetaDescription,
                orderNumber = blogDetails.ShowOrder,
                pictureSrc = blogDetails.PictureSrc ?? "assets/img/upload-icon-flat-vector-download-260nw-1378175036.jpg",
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

        private string GetAuthorName(long id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
                return user.FirstName + " " + user.LastName;
            return "سامانه چاله چوله";
        }

        private string GetAuthorIconSrc(long id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
                return user.IconSrc;
            return "assets/img/favicon-32x32.png";
        }

        private string GetBlogCategoryName(long blogCategoryId)
        {
            return _context.BlogCategories.Find(blogCategoryId).Name;
        }

        private string GetAuthorDescription(long id)
        {
            var user = _context.Users.Include(_ => _.ExpertInformation).FirstOrDefault(_ => _.Id == id);

            if (user != null)
                return user.ExpertInformation.Bio;
            return "سامانه یکپارچه چاله چوله";
        }
    }
}
