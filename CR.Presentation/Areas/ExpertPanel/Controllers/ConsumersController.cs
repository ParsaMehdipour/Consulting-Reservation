using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class ConsumersController : Controller
    {
        private readonly IGetConsumersForExpertPanelService _getConsumersForExpertPanelService;
        private readonly IGetConsumerAppointmentsForExpertPanelService _getConsumerAppointmentsForExpertPanelService;
        private readonly IGetUserFlagService _getUserFlagService;
        private ResultCheckUserFlagService ResultCheckUserFlag;


        public ConsumersController(IHttpContextAccessor contextAccessor
         , IGetConsumersForExpertPanelService getConsumersForExpertPanelService
         , IGetConsumerAppointmentsForExpertPanelService getConsumerAppointmentsForExpertPanelService
         , IGetUserFlagService getUserFlagService)
        {
            _getConsumersForExpertPanelService = getConsumersForExpertPanelService;
            _getConsumerAppointmentsForExpertPanelService = getConsumerAppointmentsForExpertPanelService;
            _getUserFlagService = getUserFlagService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
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

            var model = _getConsumersForExpertPanelService.Execute(expertId, page, pageSize).Data;

            return View(model);
        }

        public IActionResult ConsumerDetails(long consumerId, int page = 1, int pageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerAppointmentsForExpertPanelService.Execute(expertId, consumerId, page, pageSize).Data;

            return View(model);
        }
    }
}
