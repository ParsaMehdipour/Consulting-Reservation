using CR.Core.Services.Interfaces.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class AboutUsController : Controller
    {
        private readonly IGetAboutUsContentService _getAboutUsContentService;

        public AboutUsController(IGetAboutUsContentService getAboutUsContentService)
        {
            _getAboutUsContentService = getAboutUsContentService;
        }

        public IActionResult Index()
        {
            var model = _getAboutUsContentService.Execute().Data;

            return View(model);
        }
    }
}
