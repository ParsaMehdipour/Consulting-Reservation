using CR.Core.Services.Interfaces.Rules;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class RulesController : Controller
    {
        private readonly IGetRuleForPresentationService _getRuleForPresentationService;

        public RulesController(IGetRuleForPresentationService getRuleForPresentationService)
        {
            _getRuleForPresentationService = getRuleForPresentationService;
        }

        public IActionResult Index()
        {
            var model = _getRuleForPresentationService.Execute(RuleType.Full).Data;

            return View(model);
        }
        public IActionResult Payment()
        {
            var model = _getRuleForPresentationService.Execute(RuleType.Payment).Data;

            return View(model);
        }
        public IActionResult Comment()
        {
            var model = _getRuleForPresentationService.Execute(RuleType.Comment).Data;

            return View(model);
        }
        public IActionResult Other()
        {
            var model = _getRuleForPresentationService.Execute(RuleType.Other).Data;

            return View(model);
        }
    }
}
