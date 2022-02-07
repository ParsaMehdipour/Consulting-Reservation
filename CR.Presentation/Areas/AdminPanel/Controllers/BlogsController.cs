using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class BlogsController : Controller
    {
        private readonly IGetBlogsForAdminPanelService _getBlogsForAdminPanelService;
        private readonly IGetBlogCategoriesForDropdownService _getBlogCategoriesForDropdownService;

        public BlogsController(IGetBlogsForAdminPanelService getBlogsForAdminPanelService
        , IGetBlogCategoriesForDropdownService getBlogCategoriesForDropdownService)
        {
            _getBlogsForAdminPanelService = getBlogsForAdminPanelService;
            _getBlogCategoriesForDropdownService = getBlogCategoriesForDropdownService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {

            var model = _getBlogsForAdminPanelService.Execute(page, pageSize).Data;

            return View(model);
        }

        public IActionResult AddNewBlog()
        {
            ViewBag.Categories = new SelectList(_getBlogCategoriesForDropdownService.Execute(), "Id", "Name");

            return View();
        }
    }
}
