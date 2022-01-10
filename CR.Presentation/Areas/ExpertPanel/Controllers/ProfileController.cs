using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CR.Common.Utilities;
using CR.Core.DTOs.Account;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs;
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
        private readonly IGetExpertDetailsForProfileService _getExpertDetailsForSiteService;
        private readonly IEditExpertDetailsService _editExpertDetailsService;
        private readonly UserManager<User> _userManager;
        private readonly IGetSpecialtiesForExpertProfileDropDownService _getSpecialtiesForExpertProfileDropDownService;
        private ResultCheckUserFlagService ResultCheckUserFlag;

        public ProfileController(IHttpContextAccessor contextAccessor
            , IGetUserFlagService getUserFlagService
            ,IGetExpertDetailsForProfileService getExpertDetailsForSiteService
            ,IEditExpertDetailsService editExpertDetailsService
            ,UserManager<User> userManager
            ,IGetSpecialtiesForExpertProfileDropDownService getSpecialtiesForExpertProfileDropDownService)
        {
            _getExpertDetailsForSiteService = getExpertDetailsForSiteService;
            _editExpertDetailsService = editExpertDetailsService;
            _userManager = userManager;
            _getSpecialtiesForExpertProfileDropDownService = getSpecialtiesForExpertProfileDropDownService;

            _getUserFlagService = getUserFlagService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index()
        {
            if (ResultCheckUserFlag.UserFlag != UserFlag.Expert)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ViewBag.Specialties = new SelectList(_getSpecialtiesForExpertProfileDropDownService.Execute().Data, "Id", "Name");

            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getExpertDetailsForSiteService.Execute(userId).Data;

            return View(model);
        }


        [HttpPost]
        public IActionResult EditProfile(ExpertDetailsForProfileDto request)
        {

            var result = _editExpertDetailsService.Execute(request);

            return new JsonResult(result);
        }


    }
}
