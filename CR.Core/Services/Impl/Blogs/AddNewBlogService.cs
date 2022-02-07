﻿using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Blogs;
using System;

namespace CR.Core.Services.Impl.Blogs
{
    public class AddNewBlogService : IAddNewBlogService
    {
        private readonly ApplicationContext _context;

        public AddNewBlogService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewBlogDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var blog = new Blog()
                {
                    BlogCategory = GetBlogCategory(request.blogCategoryId),
                    BlogCategoryId = request.blogCategoryId,
                    CanonicalAddress = request.canonicalAddress,
                    Description = request.description,
                    MetaDescription = request.metaDescription,
                    ShowOrder = request.orderNumber,
                    Keywords = request.keyWords,
                    PublishDate = request.publishDate.ToGeorgianDateTime(),
                };

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