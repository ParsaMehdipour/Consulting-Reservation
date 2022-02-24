using CR.Common.ActiveMenus;
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
        public AppointmentsController(IGetAllAppointmentsForAdminPanelService getAllAppointmentsService)
        {
            _getAllAppointmentsService = getAllAppointmentsService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Appointments;

            var model = _getAllAppointmentsService.Execute(page, pageSize).Data;

            return View(model);
        }

        //[HttpPost]
        //public IActionResult ChangeAppointmentStatus(long appointmentId, bool activeStatus)
        //{
        //    var result = _changeAppointmentStatusService.Execute(appointmentId, activeStatus);

        //    return new JsonResult(result);
        //}
    }
}
