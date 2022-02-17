using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Blogs;
using System;

namespace CR.Core.Services.Implementations.Blogs
{
    public class AddNewBlogService : IAddNewBlogService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewBlogService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestAddNewBlogDto request, long userId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var blog = new Blog()
                {
                    Title = request.title,
                    UserId = userId,
                    Slug = request.slug,
                    ShortDescription = request.shortDescription,
                    BlogCategory = GetBlogCategory(request.blogCategoryId),
                    BlogCategoryId = request.blogCategoryId,
                    CanonicalAddress = request.canonicalAddress,
                    Description = request.description,
                    MetaDescription = request.metaDescription,
                    ShowOrder = request.orderNumber,
                    Keywords = request.keyWords,
                    PublishDate = request.publishDate.ToGeorgianDateTime(),
                };

                if (request.file != null)
                {
                    blog.PictureSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.file,
                        Folder = "Blogs"
                    });
                }

                _context.Blogs.Add(blog);
                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مقاله با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private BlogCategory GetBlogCategory(long categoryId)
        {
            return _context.BlogCategories.Find(categoryId);
        }
    }
}
