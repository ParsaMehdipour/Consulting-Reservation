using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class RulesController : Controller
    {
        private readonly IGetRulesContentService _getRulesContentService;

        public RulesController(IGetRulesContentService getRulesContentService)
        {
            _getRulesContentService = getRulesContentService;
        }

        public IActionResult Index()
        {
            TempData["activemenu"] = ActiveMenu.Rules;

            var model = _getRulesContentService.Execute().Data;

            return View(model);
        }
    }
}
