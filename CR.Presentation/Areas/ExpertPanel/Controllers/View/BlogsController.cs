using CR.Common.Utilities;
using CR.Core.Services.Interfaces.BlogCategories;
using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.View
{
    [Authorize]
    [Area("ExpertPanel")]
    public class BlogsController : Controller
    {
        private readonly IGetBlogsForExpertPanelService _getBlogsForExpertPanelService;
        private readonly IGetBlogCategoriesForDropdownService _getBlogCategoriesForDropdownService;

        public BlogsController(IGetBlogsForExpertPanelService getBlogsForExpertPanelService
        , IGetBlogCategoriesForDropdownService getBlogCategoriesForDropdownService)
        {
            _getBlogsForExpertPanelService = getBlogsForExpertPanelService;
            _getBlogCategoriesForDropdownService = getBlogCategoriesForDropdownService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getBlogsForExpertPanelService.Execute(userId, page, pageSize).Data;

            return View(model);
        }

        public IActionResult AddNewBlog()
        {
            ViewBag.Categories = new SelectList(_getBlogCategoriesForDropdownService.Execute(), "Id", "Name");

            return View();
        }
    }
}
