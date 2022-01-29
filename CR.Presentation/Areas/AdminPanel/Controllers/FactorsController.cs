using CR.Core.Services.Interfaces.Factors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class FactorsController : Controller
    {
        private readonly IGetAllFactorsForAdminPanelService _getAllFactorsForAdminPanel;

        public FactorsController(IGetAllFactorsForAdminPanelService getAllFactorsForAdminPanel)
        {
            _getAllFactorsForAdminPanel = getAllFactorsForAdminPanel;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = _getAllFactorsForAdminPanel.Execute(page, pageSize).Data;

            return View(model);
        }
    }
}
