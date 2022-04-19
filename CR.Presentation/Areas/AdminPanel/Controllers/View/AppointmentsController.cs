using CR.Common.ActiveMenus;
using CR.Core.DTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
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

        public AppointmentsController(IGetAllAppointmentsForAdminPanelService getAllAppointmentsService
        , IGetAppointmentDetailsForExpertPanelService getAppointmentDetailsForExpertPanelService)
        {
            _getAllAppointmentsService = getAllAppointmentsService;
            _getAppointmentDetailsForExpertPanelService = getAppointmentDetailsForExpertPanelService;
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
    }
}
