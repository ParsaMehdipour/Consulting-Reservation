﻿using CR.Core.Services.Interfaces.BlogCategories;
using CR.Core.Services.Interfaces.Blogs;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class BlogsController : Controller
    {
        private readonly IGetBlogsForPresentationService _getBlogsForPresentationService;
        private readonly IGetBlogDetailsForPresentationService _getBlogDetailsForPresentationService;
        private readonly IGetLatestBlogsForSiteService _getLatestBlogsForSiteService;
        private readonly IGetBlogCategoriesForSideBarService _getBlogCategoriesForSideBarService;

        public BlogsController(IGetBlogsForPresentationService getBlogsForPresentationService
            , IGetBlogDetailsForPresentationService getBlogDetailsForPresentationService
            , IGetLatestBlogsForSiteService getLatestBlogsForSiteService
            , IGetBlogCategoriesForSideBarService getBlogCategoriesForSideBarService)
        {
            _getBlogsForPresentationService = getBlogsForPresentationService;
            _getBlogDetailsForPresentationService = getBlogDetailsForPresentationService;
            _getLatestBlogsForSiteService = getLatestBlogsForSiteService;
            _getBlogCategoriesForSideBarService = getBlogCategoriesForSideBarService;
        }

        public IActionResult Index(string searchKey, int page = 1, int pageSize = 20)
        {
            var model = new BlogsIndexViewModel
            {
                ResultGetBlogsForPresentationDto = _getBlogsForPresentationService.Execute(searchKey, page, pageSize).Data,
                LatestBlogsDto = _getLatestBlogsForSiteService.Execute().Data,
                BlogCategoryForSideBarDtos = _getBlogCategoriesForSideBarService.Execute().Data
            };

            return View(model);
        }

        public IActionResult BlogDetails(string slug)
        {
            var model = new BlogDetailsViewModel
            {
                BlogDetailsForPresentationDto = _getBlogDetailsForPresentationService.Execute(slug).Data,
                LatestBlogsDto = _getLatestBlogsForSiteService.Execute().Data,
                BlogCategoryForSideBarDtos = _getBlogCategoriesForSideBarService.Execute().Data
            };

            return View(model);
        }
    }
}
