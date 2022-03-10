using CR.Core.DTOs.RequestDTOs.Rule;
using CR.Core.Services.Interfaces.Rules;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly ICreateRuleService _createRuleService;
        private readonly IEditRuleService _editRuleService;

        public RulesController(ICreateRuleService createRuleService
        , IEditRuleService editRuleService)
        {
            _createRuleService = createRuleService;
            _editRuleService = editRuleService;
        }

        [Route("/api/Rules/Create")]
        [HttpPost]
        public IActionResult Create([FromForm] RequestCreateRuleDto request)
        {
            var result = _createRuleService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/Rules/Edit")]
        [HttpPost]
        public IActionResult Edit([FromForm] RequestEditRuleDto request)
        {
            var result = _editRuleService.Execute(request);

            return new JsonResult(result);
        }
    }
}
