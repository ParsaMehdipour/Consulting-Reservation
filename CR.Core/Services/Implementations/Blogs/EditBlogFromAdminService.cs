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
    public class EditBlogFromAdminService : IEditBlogFromAdminService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditBlogFromAdminService(ApplicationContext context,
            IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestEditBlogDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var blog = _context.Blogs.Find(request.id);

                if (blog == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مقاله یافت نشد!!"
                    };
                }

                blog.Title = request.title;
                blog.Slug = request.slug;
                blog.Description = request.description;
                blog.Keywords = request.keyWords;
                blog.MetaDescription = request.metaDescription;
                blog.PublishDate = request.publishDate.ToGeorgianDateTime();
                blog.ShortDescription = request.shortDescription;
                blog.ShowOrder = request.orderNumber;
                blog.BlogCategoryId = request.blogCategoryId;
                blog.Status = request.status;
                blog.BlogCategory = GetBlogCategory(request.blogCategoryId);
                blog.CanonicalAddress = request.canonicalAddress;

                if (request.file != null)
                {
                    blog.PictureSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        Folder = "Blogs",
                        File = request.file
                    });
                }

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مقاله با موفقیت ویرایش شد"
                };

            }
            catch (Exception)
            {

                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private BlogCategory GetBlogCategory(long id)
        {
            return _context.BlogCategories.Find(id);
        }
    }
}
