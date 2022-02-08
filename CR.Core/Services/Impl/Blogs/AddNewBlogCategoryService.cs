using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Blogs;
using System;

namespace CR.Core.Services.Impl.Blogs
{
    public class AddNewBlogCategoryService : IAddNewBlogCategoryService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewBlogCategoryService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestAddNewBlogCategoryDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var parentCategory = GetParentCategory(request.parentId);

                var blogCategory = new BlogCategory()
                {
                    Name = request.name,
                    Description = request.description,
                    MetaDescription = request.metaDescription,
                    PictureSrc = (request.file != null) ? _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.file,
                        Folder = "BlogCategories"
                    }) : "assets/img/layers.png",
                    ShowOrder = request.showOrder,
                    Slug = request.slug,
                    ParentCategory = parentCategory,
                };

                //if (parentCategory != null)
                //{
                //    parentCategory.SubCategories.Add(blogCategory);
                //}

                _context.BlogCategories.Add(blogCategory);
                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "گروه مقاله با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل از سمت سرور"
                };
            }
        }

        public BlogCategory GetParentCategory(long? parentId)
        {
            return _context.BlogCategories.Find(parentId);
        }
    }
}
