using CR.Common.ActiveMenus;
using CR.Common.Utilities;
using CR.Core.DTOs.Account;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Statistics;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Users;
using CR.Presentation.Areas.AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly IGetLatestExpertsForAdminService _getLatestExpertsForAdminService;
        private readonly IGetLatestConsumersForAdminService _getLatestConsumersForAdminService;
        private readonly IGetAdminDetailsService _getAdminDetailsService;
        private readonly IEditAdminDetailsService _editAdminDetailsService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IGetStatisticsForAdminPanelService _getStatisticsForAdminPanelService;
        private readonly IGetAppointmentsForAdminDashboardService _getAppointmentsForAdminDashboardService;

        public HomeController(IGetLatestExpertsForAdminService getLatestExpertsForAdminService
        , IGetLatestConsumersForAdminService getLatestConsumersForAdminService
        , IGetAdminDetailsService getAdminDetailsService
        , IEditAdminDetailsService editAdminDetailsService
        , UserManager<User> userManager
        , ApplicationContext context
        , IGetStatisticsForAdminPanelService getStatisticsForAdminPanelService
        , IGetAppointmentsForAdminDashboardService getAppointmentsForAdminDashboardService)
        {
            _getLatestExpertsForAdminService = getLatestExpertsForAdminService;
            _getLatestConsumersForAdminService = getLatestConsumersForAdminService;
            _getAdminDetailsService = getAdminDetailsService;
            _editAdminDetailsService = editAdminDetailsService;
            _userManager = userManager;
            _context = context;
            _getStatisticsForAdminPanelService = getStatisticsForAdminPanelService;
            _getAppointmentsForAdminDashboardService = getAppointmentsForAdminDashboardService;
        }

        public IActionResult Index()
        {
            TempData["activemenu"] = ActiveMenu.Dashboard;


            var viewModel = new AdminDashboardViewModel()
            {
                LatestExpertForAdminDtos = _getLatestExpertsForAdminService.Execute().Data,
                LatestConsumerForAdminDtos = _getLatestConsumersForAdminService.Execute().Data,
                StatisticsDto = _getStatisticsForAdminPanelService.Execute().Data,
                AppointmentForAdminDtos = _getAppointmentsForAdminDashboardService.Execute().Data
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            TempData["activemenu"] = ActiveMenu.Dashboard;

            var adminId = ClaimUtility.GetUserId(User).Value;

            var model = _getAdminDetailsService.Execute(adminId).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfileDetails(AdminDetailsDto request)
        {
            var result = _editAdminDetailsService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordFromAdmin request)
        {
            if (ModelState.IsValid)
            {
                request.UserId = ClaimUtility.GetUserId(User).Value;

                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                var password = new PasswordHasher<User>();
                user.PasswordHash = password.HashPassword(null, request.newPassword);

                _context.SaveChanges();

                ViewData["ErrorMessage"] = "رمزعبور شما با موفقیت تغییر یافت";
                return View("Profile");


            }

            return View("Profile");
        }
    }
}
