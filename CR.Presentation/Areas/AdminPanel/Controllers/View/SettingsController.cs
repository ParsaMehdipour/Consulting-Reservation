using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingServices _settingServices;

        public SettingsController(ISettingServices settingServices)
        {
            _settingServices = settingServices;
        }

        public IActionResult Index()
        {
            TempData["activemenu"] = ActiveMenu.Settings;

            var model = _settingServices.Get().Data;

            return View(model);
        }
    }
}
