using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class BlogCategoriesController : Controller
    {
        private readonly IGetBlogCategoriesForAdminPanelService _getBlogCategoriesForAdminPanelService;

        public BlogCategoriesController(IGetBlogCategoriesForAdminPanelService getBlogCategoriesForAdminPanelService)
        {
            _getBlogCategoriesForAdminPanelService = getBlogCategoriesForAdminPanelService;
        }

        public IActionResult Index(long? parentId, int page = 1, int pageSize = 20)
        {
            var model = _getBlogCategoriesForAdminPanelService.Execute(parentId, page, pageSize).Data;

            return View(model);
        }
    }
}
