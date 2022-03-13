using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class BlogCommentsController : Controller
    {
        private readonly IGetBlogCommentsForAdminService _getBlogCommentsForAdminService;

        public BlogCommentsController(IGetBlogCommentsForAdminService getBlogCommentsForAdminService)
        {
            _getBlogCommentsForAdminService = getBlogCommentsForAdminService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.BlogComments;

            var model = _getBlogCommentsForAdminService.Execute(page, pageSize).Data;

            return View(model);
        }
    }
}
