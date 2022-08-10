using CR.Common.ActiveMenus;
using CR.Core.DTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.ChatUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAllAppointmentsForAdminPanelService _getAllAppointmentsService;
        private readonly IGetAppointmentDetailsForExpertPanelService _getAppointmentDetailsForExpertPanelService;
        private readonly IGetExpertChatUsersService _getExpertChatUsersService;

        public AppointmentsController(IGetAllAppointmentsForAdminPanelService getAllAppointmentsService
        , IGetAppointmentDetailsForExpertPanelService getAppointmentDetailsForExpertPanelService, IGetExpertChatUsersService getExpertChatUsersService)
        {
            _getAllAppointmentsService = getAllAppointmentsService;
            _getAppointmentDetailsForExpertPanelService = getAppointmentDetailsForExpertPanelService;
            _getExpertChatUsersService = getExpertChatUsersService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Appointments;

            var model = _getAllAppointmentsService.Execute(page, pageSize).Data;

            return View(model);
        }

        [HttpPost]
        public AppointmentDetailsForExpertPanel GetDetails(long id)
        {
            return _getAppointmentDetailsForExpertPanelService.Execute(id).Data;
        }

        public IActionResult ChatDetails(long expertId)
        {
            TempData["activemenu"] = ActiveMenu.Appointments;

            var model = _getExpertChatUsersService.Execute(expertId, "").Data;

            return View(model);
        }
    }
}
