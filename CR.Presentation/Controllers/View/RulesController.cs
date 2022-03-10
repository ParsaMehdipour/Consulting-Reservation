using CR.Core.Services.Interfaces.Rules;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class RulesController : Controller
    {
        private readonly IGetRulesContentService _getRulesContentService;

        public RulesController(IGetRulesContentService getRulesContentService)
        {
            _getRulesContentService = getRulesContentService;
        }

        public IActionResult Index()
        {
            var model = _getRulesContentService.Execute().Data;

            return View(model);
        }
    }
}
