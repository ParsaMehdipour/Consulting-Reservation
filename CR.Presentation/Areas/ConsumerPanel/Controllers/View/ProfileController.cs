using CR.Common.Convertor;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class ProfileController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IAddConsumerDetailsService _addConsumerDetailsService;
        private readonly IEditConsumerDetailsService _editConsumerDetailsService;
        private readonly IGetConsumerDetailsForProfileService _getConsumerDetailsForSiteService;
        private ResultCheckUserFlagService ResultCheckUserFlag;

        public ProfileController(IHttpContextAccessor contextAccessor
        ,IGetUserFlagService getUserFlagService
        ,IAddConsumerDetailsService addConsumerDetailsService
        ,IEditConsumerDetailsService editConsumerDetailsService
        ,IGetConsumerDetailsForProfileService getConsumerDetailsForSiteService)
        {
            _getUserFlagService = getUserFlagService;
            _addConsumerDetailsService = addConsumerDetailsService;
            _editConsumerDetailsService = editConsumerDetailsService;
            _getConsumerDetailsForSiteService = getConsumerDetailsForSiteService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index()
        {
            if (ResultCheckUserFlag.UserFlag != UserFlag.Consumer)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerDetailsForSiteService.Execute(userId).Data.ConsumerDetailsForSiteDto;

            return View(model);
        }


        [HttpPost]
        public IActionResult AddProfileDetails(RequestAddConsumerDetailsDto request)
        {
            request.consumerId = ClaimUtility.GetUserId(User).Value;

            var result = _addConsumerDetailsService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult EditProfileDetails(RequestEditConsumerDetailsDto request)
        {
            request.ConsumerId = ClaimUtility.GetUserId(User).Value;

            var result = _editConsumerDetailsService.Execute(request);

            return new JsonResult(result);
        }
    }
}
