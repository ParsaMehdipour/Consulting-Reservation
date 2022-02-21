using CR.Common.ActiveMenus;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.ExpertImages;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
        private readonly IGetExpertDetailsForProfileService _getExpertDetailsForProfileService;
        private readonly IGetSpecialtiesForExpertProfileDropDownService _getSpecialtiesForExpertProfileDropDownService;
        private readonly IEditBasicExpertDetailsService _editBasicExpertDetailsService;
        private readonly IEditAdvancedExpertDetailsService _editAdvancedExpertDetailsService;
        private readonly IRemoveExpertImagesService _removeExpertImagesService;

        public ExpertsController(IGetAllExpertsService getAllExpertsService
        , IChangeExpertStatusService changeExpertStatusService
        , IRegisterExpertFromAdminService registerExpertFromAdminService
        , IGetExpertDetailsForAdminService getExpertDetailsForAdminService
        , IEditExpertDetailsFromAdminService editExpertDetailsFromAdminService
        , IGetExpertDetailsForProfileService getExpertDetailsForProfileService
        , IGetSpecialtiesForExpertProfileDropDownService getSpecialtiesForExpertProfileDropDownService
        , IEditBasicExpertDetailsService editBasicExpertDetailsService
        , IEditAdvancedExpertDetailsService editAdvancedExpertDetailsService
        , IRemoveExpertImagesService removeExpertImagesService)
        {
            _getAllExpertsService = getAllExpertsService;
            _changeExpertStatusService = changeExpertStatusService;
            _registerExpertFromAdminService = registerExpertFromAdminService;
            _getExpertDetailsForAdminService = getExpertDetailsForAdminService;
            _editExpertDetailsFromAdminService = editExpertDetailsFromAdminService;
            _getExpertDetailsForProfileService = getExpertDetailsForProfileService;
            _getSpecialtiesForExpertProfileDropDownService = getSpecialtiesForExpertProfileDropDownService;
            _editBasicExpertDetailsService = editBasicExpertDetailsService;
            _editAdvancedExpertDetailsService = editAdvancedExpertDetailsService;
            _removeExpertImagesService = removeExpertImagesService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Experts;


            var model = _getAllExpertsService.Execute(page, pageSize).Data;

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
            TempData["activemenu"] = ActiveMenu.Experts;

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
            TempData["activemenu"] = ActiveMenu.Experts;

            ViewBag.Specialties = new SelectList(_getSpecialtiesForExpertProfileDropDownService.Execute().Data, "Id", "Name");

            var model = _getExpertDetailsForProfileService.Execute(id).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditBasicInformation(RequestEditBasicExpertDetailsDto request)
        {
            var result = _editBasicExpertDetailsService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult EditAdvancedInformation(RequestEditAdvancedExpertDetailsDto request)
        {
            var result = _editAdvancedExpertDetailsService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult RemoveExpertImage(long id)
        {
            var result = _removeExpertImagesService.Execute(id);

            return new JsonResult(result);
        }
    }
}
