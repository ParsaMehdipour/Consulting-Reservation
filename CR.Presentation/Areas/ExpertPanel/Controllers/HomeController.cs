using System.Collections.Generic;
using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Statistics;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using CR.Presentation.Areas.ExpertPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class HomeController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IGetTodayConsumersForExpertDashboardService _getTodayConsumersForExpertDashboardService;
        private readonly IGetIncomingConsumersForExpertDashboardService _getIncomingConsumersForExpertDashboardService;
        private readonly IGetStatisticsForExpertPanelService _getStatisticsForExpertPanelService;
        private ResultCheckUserFlagService ResultCheckUserFlag;

        public HomeController(IHttpContextAccessor contextAccessor
            , IGetUserFlagService getUserFlagService
            ,IGetTodayConsumersForExpertDashboardService getTodayConsumersForExpertDashboardService
            ,IGetIncomingConsumersForExpertDashboardService getIncomingConsumersForExpertDashboardService
            ,IGetStatisticsForExpertPanelService getStatisticsForExpertPanelService)
        {
            _getUserFlagService = getUserFlagService;
            _getTodayConsumersForExpertDashboardService = getTodayConsumersForExpertDashboardService;
            _getIncomingConsumersForExpertDashboardService = getIncomingConsumersForExpertDashboardService;
            _getStatisticsForExpertPanelService = getStatisticsForExpertPanelService;

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

            var viewModel = new ExpertPanelDashboardViewModel()
            {
                ResultGetTodayConsumersForExpertDashboardDto = _getTodayConsumersForExpertDashboardService
                    .Execute(expertId, page, pageSize).Data,
                ResultGetIncomingConsumersForExpertDashboardDto = _getIncomingConsumersForExpertDashboardService.Execute(expertId,page,pageSize).Data,
                StatisticsForExpertPanelDto = _getStatisticsForExpertPanelService.Execute(expertId)
            };

            return View(viewModel);
        }
    }
}