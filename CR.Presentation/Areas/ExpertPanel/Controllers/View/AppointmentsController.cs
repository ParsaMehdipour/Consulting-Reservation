using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.View
{
    [Authorize]
    [Area("ExpertPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAllAppointmentsForExpertPanelService _getAllAppointmentsForExpertPanel;
        private readonly IGetAppointmentDetailsForExpertPanelService _getAppointmentDetailsForExpertPanelService;
        private readonly IChangeAppointmentStatusService _changeAppointmentStatusService;
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly IAddChargeExpertWalletService _addChargeExpertWalletService;
        private ResultCheckUserFlagService ResultCheckUserFlag;

        public AppointmentsController(IHttpContextAccessor contextAccessor
        , IGetAllAppointmentsForExpertPanelService getAllAppointmentsForExpertPanel
        , IGetAppointmentDetailsForExpertPanelService getAppointmentDetailsForExpertPanelService
        , IChangeAppointmentStatusService changeAppointmentStatusService
        , IGetUserFlagService getUserFlagService
        , IAddChargeExpertWalletService addChargeExpertWalletService)
        {
            _getAllAppointmentsForExpertPanel = getAllAppointmentsForExpertPanel;
            _getAppointmentDetailsForExpertPanelService = getAppointmentDetailsForExpertPanelService;
            _changeAppointmentStatusService = changeAppointmentStatusService;
            _getUserFlagService = getUserFlagService;
            _addChargeExpertWalletService = addChargeExpertWalletService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
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

            var model = _getAllAppointmentsForExpertPanel.Execute(expertId, Page, PageSize).Data;

            return View(model);
        }

        [HttpPost]
        public AppointmentDetailsForExpertPanel GetDetails(long id)
        {
            return _getAppointmentDetailsForExpertPanelService.Execute(id).Data;
        }

        public IActionResult ChangeAppointmentStatus(RequestChangeAppointmentStatusDto request)
        {
            var result = _changeAppointmentStatusService.Execute(request);

            if (result.IsSuccess == true)
            {
                return new JsonResult(_addChargeExpertWalletService.Execute(result.Data.receiverId, result.Data.price));
            }

            return new JsonResult(result);
        }
    }
}
