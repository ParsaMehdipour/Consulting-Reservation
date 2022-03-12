using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class ContactUsController : Controller
    {
        private readonly IGetContactUsForAdminPanelService _getContactUsForAdminPanelService;

        public ContactUsController(IGetContactUsForAdminPanelService getContactUsForAdminPanelService)
        {
            _getContactUsForAdminPanelService = getContactUsForAdminPanelService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.ContactUs;


            var model = _getContactUsForAdminPanelService.Execute(page, pageSize).Data;

            return View(model);
        }
    }
}
