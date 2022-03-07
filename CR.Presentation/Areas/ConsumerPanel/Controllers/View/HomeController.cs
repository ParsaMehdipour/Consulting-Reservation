using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using CR.Presentation.Areas.ConsumerPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.View
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class HomeController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IGetAllAppointmentsForConsumerPanelService _getAllAppointmentsForConsumerPanelService;
        private ResultCheckUserFlagService resultCheckUserFlag;

        public HomeController(IGetUserFlagService getUserFlagService
            ,IHttpContextAccessor contextAccessor
            ,IGetAllAppointmentsForConsumerPanelService getAllAppointmentsForConsumerPanelService)
        {
            _getUserFlagService = getUserFlagService;
            _getAllAppointmentsForConsumerPanelService = getAllAppointmentsForConsumerPanelService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext.User);

            resultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            if (resultCheckUserFlag.UserFlag != UserFlag.Consumer)
                return RedirectToAction("Index", "Home", new { area = "" });

            var consumerId = ClaimUtility.GetUserId(User).Value;

            var DashboardViewModel = new ConsumerPanelDashboardViewModel()
            {
                ResultGetAllAppointmentsForConsumerPanelDto =
                    _getAllAppointmentsForConsumerPanelService.Execute(consumerId,page, pageSize).Data
            };


            return View(DashboardViewModel);

        }

    }
}
