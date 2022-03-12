using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class ContactUsContentController : Controller
    {
        private readonly IGetContactUsContentService _getContactUsContentService;

        public ContactUsContentController(IGetContactUsContentService getContactUsContentService)
        {
            _getContactUsContentService = getContactUsContentService;
        }

        public IActionResult Index()
        {
            TempData["activemenu"] = ActiveMenu.ContactUsContent;

            var model = _getContactUsContentService.Execute().Data;

            return View(model);
        }
    }
}
