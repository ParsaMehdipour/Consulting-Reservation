using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.Comments;
using CR.Presentation.Areas.AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class BlogsController : Controller
    {
        private readonly IGetBlogsForAdminPanelService _getBlogsForAdminPanelService;
        private readonly IGetBlogCategoriesForDropdownService _getBlogCategoriesForDropdownService;
        private readonly IGetBlogDetailsService _getBlogDetailsService;
        private readonly IGetBlogCommentsForBlogDetailsByIdService _getBlogCommentsForBlogDetailsByIdService;

        public BlogsController(IGetBlogsForAdminPanelService getBlogsForAdminPanelService
        , IGetBlogCategoriesForDropdownService getBlogCategoriesForDropdownService
        , IGetBlogDetailsService getBlogDetailsService
        , IGetBlogCommentsForBlogDetailsByIdService getBlogCommentsForBlogDetailsByIdService)
        {
            _getBlogsForAdminPanelService = getBlogsForAdminPanelService;
            _getBlogCategoriesForDropdownService = getBlogCategoriesForDropdownService;
            _getBlogDetailsService = getBlogDetailsService;
            _getBlogCommentsForBlogDetailsByIdService = getBlogCommentsForBlogDetailsByIdService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Blogs;

            var model = _getBlogsForAdminPanelService.Execute(page, pageSize).Data;

            return View(model);
        }

        public IActionResult AddNewBlog()
        {
            TempData["activemenu"] = ActiveMenu.Blogs;

            ViewBag.Categories = new SelectList(_getBlogCategoriesForDropdownService.Execute(), "Id", "Name");

            return View();
        }

        public IActionResult EditBlogDetails(long id)
        {
            TempData["activemenu"] = ActiveMenu.Blogs;

            var model = _getBlogDetailsService.Execute(id).Data;

            ViewBag.Categories = new SelectList(_getBlogCategoriesForDropdownService.Execute(), "Id", "Name");

            return View(model);
        }

        public IActionResult BlogDetails(long id)
        {
            TempData["activemenu"] = ActiveMenu.Blogs;

            var model = new AdminBlogDetailsViewModel()
            {
                BlogDetailsDto = _getBlogDetailsService.Execute(id).Data,
                BlogCommentDtos = _getBlogCommentsForBlogDetailsByIdService.Execute(id).Data
            };

            return View(model);
        }
    }
}
