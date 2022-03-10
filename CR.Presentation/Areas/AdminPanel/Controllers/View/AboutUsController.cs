using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.AboutUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class AboutUsController : Controller
    {
        private readonly IGetAboutUsContentService _getAboutUsContentService;

        public AboutUsController(IGetAboutUsContentService getAboutUsContentService)
        {
            _getAboutUsContentService = getAboutUsContentService;
        }

        public IActionResult Index()
        {
            TempData["activemenu"] = ActiveMenu.AboutUs;

            var model = _getAboutUsContentService.Execute().Data;

            return View(model);
        }
    }
}
