using CR.Core.Services.Interfaces.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAllAppointmentsForAdminPanelService _getAllAppointmentsService;
        private readonly IChangeAppointmentStatusService _changeAppointmentStatusService;

        public AppointmentsController(IGetAllAppointmentsForAdminPanelService getAllAppointmentsService
        ,IChangeAppointmentStatusService changeAppointmentStatusService)
        {
            _getAllAppointmentsService = getAllAppointmentsService;
            _changeAppointmentStatusService = changeAppointmentStatusService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {

            var model = _getAllAppointmentsService.Execute(page,pageSize).Data;

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
