using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.ExpertImages;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class ProfileController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IGetExpertDetailsForProfileService _getExpertDetailsForProfileService;
        private readonly UserManager<User> _userManager;
        private readonly IGetSpecialtiesForExpertProfileDropDownService _getSpecialtiesForExpertProfileDropDownService;
        private readonly IEditBasicExpertDetailsService _editBasicExpertDetailsService;
        private readonly IEditAdvancedExpertDetailsService _editAdvancedExpertDetailsService;
        private readonly IRemoveExpertImagesService _removeExpertImagesService;
        private ResultCheckUserFlagService ResultCheckUserFlag;

        public ProfileController(IHttpContextAccessor contextAccessor
            , IGetUserFlagService getUserFlagService
            ,IGetExpertDetailsForProfileService getExpertDetailsForProfileService
            ,UserManager<User> userManager
            ,IGetSpecialtiesForExpertProfileDropDownService getSpecialtiesForExpertProfileDropDownService
            ,IEditBasicExpertDetailsService editBasicExpertDetailsService
            ,IEditAdvancedExpertDetailsService editAdvancedExpertDetailsService
            ,IRemoveExpertImagesService removeExpertImagesService)
        {
            _getExpertDetailsForProfileService = getExpertDetailsForProfileService;
            _userManager = userManager;
            _getSpecialtiesForExpertProfileDropDownService = getSpecialtiesForExpertProfileDropDownService;
            _editBasicExpertDetailsService = editBasicExpertDetailsService;
            _editAdvancedExpertDetailsService = editAdvancedExpertDetailsService;
            _removeExpertImagesService = removeExpertImagesService;

            _getUserFlagService = getUserFlagService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (ResultCheckUserFlag.UserFlag != UserFlag.Expert)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ViewBag.Specialties = new SelectList(_getSpecialtiesForExpertProfileDropDownService.Execute().Data, "Id", "Name");

            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getExpertDetailsForProfileService.Execute(userId).Data;

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
