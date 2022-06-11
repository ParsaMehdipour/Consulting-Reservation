using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Factors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class FactorsController : Controller
    {
        private readonly IGetAllFactorsForAdminPanelService _getAllFactorsForAdminPanel;
        private readonly IGetFactorDetailsForAdminPanelService _getFactorDetailsForAdminPanelService;

        public FactorsController(IGetAllFactorsForAdminPanelService getAllFactorsForAdminPanel,
            IGetFactorDetailsForAdminPanelService getFactorDetailsForAdminPanelService)
        {
            _getAllFactorsForAdminPanel = getAllFactorsForAdminPanel;
            _getFactorDetailsForAdminPanelService = getFactorDetailsForAdminPanelService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Factors;

            var model = _getAllFactorsForAdminPanel.Execute(page, pageSize).Data;

            return View(model);
        }

        public IActionResult FactorDetails(long factorId)
        {
            TempData["activemenu"] = ActiveMenu.Factors;

            var result = _getFactorDetailsForAdminPanelService.Execute(factorId);

            if (result.IsSuccess is true)
            {
                return View(result.Data);
            }

            return RedirectToAction("Index");
        }
    }
}
