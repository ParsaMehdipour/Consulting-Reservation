using System.Threading.Tasks;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Impl.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class ExpertsController : Controller
    {
        private readonly IGetAllExpertsService _getAllExpertsService;
        private readonly IChangeExpertStatusService _changeExpertStatusService;
        private readonly IRegisterExpertFromAdminService _registerExpertFromAdminService;
        private readonly IGetExpertDetailsForAdminService _getExpertDetailsForAdminService;
        private readonly IEditExpertDetailsFromAdminService _editExpertDetailsFromAdminService;

        public ExpertsController(IGetAllExpertsService getAllExpertsService
        , IChangeExpertStatusService changeExpertStatusService
        ,IRegisterExpertFromAdminService registerExpertFromAdminService
        ,IGetExpertDetailsForAdminService getExpertDetailsForAdminService
        ,IEditExpertDetailsFromAdminService editExpertDetailsFromAdminService)
        {
            _getAllExpertsService = getAllExpertsService;
            _changeExpertStatusService = changeExpertStatusService;
            _registerExpertFromAdminService = registerExpertFromAdminService;
            _getExpertDetailsForAdminService = getExpertDetailsForAdminService;
            _editExpertDetailsFromAdminService = editExpertDetailsFromAdminService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = _getAllExpertsService.Execute(page,pageSize).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeExpertStatus(long expertId, bool activeStatus)
        {
            var result = _changeExpertStatusService.Execute(expertId, activeStatus);

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult AddNewExpert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpert(RegisterExpertFromAdminDto request)
        {
            var result = await _registerExpertFromAdminService.Execute(request);

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult ExpertDetails(long id)
        {
            var model = _getExpertDetailsForAdminService.Execute(id).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditExpertDetails(ExpertDetailsForAdminDto request)
        {
            var result = _editExpertDetailsFromAdminService.Execute(request);

            return new JsonResult(result);
        }
    }
}
