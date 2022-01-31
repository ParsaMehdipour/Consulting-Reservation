using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Factors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class FactorsController : Controller
    {
        private readonly IGetAllFactorsForExpertPanelService _getAllFactorsForExpertPanelService;

        public FactorsController(IGetAllFactorsForExpertPanelService getAllFactorsForExpertPanelService)
        {
            _getAllFactorsForExpertPanelService = getAllFactorsForExpertPanelService;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getAllFactorsForExpertPanelService.Execute(expertId, Page, PageSize).Data;

            return View(model);
        }
    }
}
