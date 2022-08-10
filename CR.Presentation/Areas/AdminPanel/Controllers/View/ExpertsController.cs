using CR.Common.ActiveMenus;
using CR.Core.DTOs.Days;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.Timings;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Days;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.ExpertImages;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.Core.Services.Interfaces.Timings;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using CR.Presentation.Areas.ExpertPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
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

        private readonly IGetDaysService _getDaysService;
        private readonly IAddDayService _addDayService;
        private readonly IAddTimeOfDayService _addTimeOfDayService;
        private readonly IRemoveTimeOfDayService _removeTimeOfDayService;
        private readonly IGetTimingsForDropDownService _getTimingsForDropDownService;
        private readonly IGetDayDetailsService _getDayDetailsService;
        private readonly IEditDayDetailsService _editDayDetailsService;

        public ExpertsController(IGetAllExpertsService getAllExpertsService
        , IChangeExpertStatusService changeExpertStatusService
        , IRegisterExpertFromAdminService registerExpertFromAdminService
        , IGetExpertDetailsForAdminService getExpertDetailsForAdminService
        , IEditExpertDetailsFromAdminService editExpertDetailsFromAdminService
        , IGetExpertDetailsForProfileService getExpertDetailsForProfileService
        , IGetSpecialtiesForExpertProfileDropDownService getSpecialtiesForExpertProfileDropDownService
        , IEditBasicExpertDetailsService editBasicExpertDetailsService
        , IEditAdvancedExpertDetailsService editAdvancedExpertDetailsService
        , IRemoveExpertImagesService removeExpertImagesService

        , IGetDaysService getDaysService
        , IAddDayService addDayService
        , IAddTimeOfDayService addTimeOfDayService
        , IRemoveTimeOfDayService removeTimeOfDayService
        , IGetTimingsForDropDownService getTimingsForDropDownService
        , IGetDayDetailsService getDayDetailsService
        , IEditDayDetailsService editDayDetailsService)
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

            _getDaysService = getDaysService;
            _addDayService = addDayService;
            _addTimeOfDayService = addTimeOfDayService;
            _removeTimeOfDayService = removeTimeOfDayService;
            _getTimingsForDropDownService = getTimingsForDropDownService;
            _getDayDetailsService = getDayDetailsService;
            _editDayDetailsService = editDayDetailsService;
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
            var result = _editBasicExpertDetailsService.Execute(request, false);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult EditAdvancedInformation(RequestEditAdvancedExpertDetailsDto request)
        {
            var result = _editAdvancedExpertDetailsService.Execute(request, false);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult RemoveExpertImage(long id)
        {
            var result = _removeExpertImagesService.Execute(id);

            return new JsonResult(result);
        }

        public IActionResult GetAvailability(long expertId)
        {
            TempData["activemenu"] = ActiveMenu.Experts;
            var result = _getDaysService.Execute(expertId).Data;
            var viewModel = new ExpertAvailabilitiesViewModel()
            {
                DayDtos = result.DayDtos,
                ExpertInformationId = result.ExpertInformationId,
                ExpertId = expertId
            };
            return View(viewModel);
        }

        [HttpPost]
        public TimingForDropDownDtos GetTimings(TimingType timingType)
        {
            var dropDownModel = _getTimingsForDropDownService.Execute(timingType).Data;
            return dropDownModel;
        }

        [HttpPost]
        public IActionResult AddDay(RequestAddNewDayDto request)
        {
            var result = _addDayService.Execute(request);
            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult AddTimeOfDay(RequestAddNewTimeOfDayDto request)
        {
            var result = _addTimeOfDayService.Execute(request);
            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult RemoveTimeOfDay(long id)
        {
            var result = _removeTimeOfDayService.Execute(id);
            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult EditDetails(RequestEditDayDetaislDto request)
        {
            var result = _editDayDetailsService.Execute(request);
            return new JsonResult(result);
        }
    }
}
