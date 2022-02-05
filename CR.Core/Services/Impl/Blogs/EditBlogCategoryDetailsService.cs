using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Impl.Blogs
{
    public class EditBlogCategoryDetailsService : IEditBlogCategoryDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditBlogCategoryDetailsService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestEditBlogCategoryDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var blogCategory = _context.BlogCategories.Find(request.id);

                if (blogCategory == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات گروه یافت نشد!!"
                    };
                }

                blogCategory.Name = request.name;
                blogCategory.Description = request.description;
                blogCategory.MetaDescription = request.metaDescription;
                blogCategory.ShowOrder = request.orderNumber;
                blogCategory.Slug = request.slug;

                if (request.file != null)
                {
                    blogCategory.PictureSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.file,
                        Folder = "BlogCategories"
                    });
                }

                _context.SaveChanges();
                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "گروه مقاله با موفقیت ویرایش شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    Message = "مشکل از سمت سرور!!",
                    IsSuccess = false
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
