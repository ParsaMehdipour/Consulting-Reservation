using System.Collections.Generic;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.Timings;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using CR.Presentation.Areas.ExpertPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class AvailabilitiesController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IAddDayService _addDayService;
        private readonly IGetDaysService _getDaysService;
        private readonly IAddTimeOfDayService _addTimeOfDayService;
        private readonly IRemoveTimeOfDayService _removeTimeOfDayService;
        private readonly IGetTimingsForDropDownService _getTimingsForDropDownService;
        private ResultCheckUserFlagService ResultCheckUserFlag;


        public AvailabilitiesController(IHttpContextAccessor contextAccessor
            , IGetUserFlagService getUserFlagService
            ,IAddDayService addDayService
            ,IGetDaysService getDaysService
            ,IAddTimeOfDayService addTimeOfDayService
            ,IRemoveTimeOfDayService removeTimeOfDayService
            ,IGetTimingsForDropDownService getTimingsForDropDownService)
        {
            _getUserFlagService = getUserFlagService;
            _addDayService = addDayService;
            _getDaysService = getDaysService;
            _addTimeOfDayService = addTimeOfDayService;
            _removeTimeOfDayService = removeTimeOfDayService;
            _getTimingsForDropDownService = getTimingsForDropDownService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index()
        {
            if (ResultCheckUserFlag.UserFlag != UserFlag.Expert)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ResultCheckUserFlag.IsActive != true)
            {
                return RedirectToAction("Index", "Profile");

            }

            var expertId = ClaimUtility.GetUserId(User).Value;

            var result = _getDaysService.Execute(expertId).Data;

            var viewModel = new ExpertAvailabilitiesViewModel()
            {
                DayDtos = result.DayDtos,
                ExpertInformationId = result.ExpertInformationId
            };

            return View(viewModel);
        }

        [HttpPost]
        public List<TimingDto> GetTimings(TimingType timingType)
        {
            var dropDownModel = _getTimingsForDropDownService.Execute(timingType).Data;

            return dropDownModel;
        }

        [HttpPost]
        public IActionResult AddDay(RequestAddNewDayDto request)
        {
            request.expertId = ClaimUtility.GetUserId(User).Value;

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
    }
}
