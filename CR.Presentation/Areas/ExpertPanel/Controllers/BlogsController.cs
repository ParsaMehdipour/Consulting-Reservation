using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class BlogsController : Controller
    {
        private readonly IGetBlogsForExpertPanelService _getBlogsForExpertPanelService;

        public BlogsController(IGetBlogsForExpertPanelService getBlogsForExpertPanelService)
        {
            _getBlogsForExpertPanelService = getBlogsForExpertPanelService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getBlogsForExpertPanelService.Execute(userId, page, pageSize).Data;

            return View(model);
        }
    }
}
