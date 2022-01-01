using System;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointment;
using CR.Core.Services.Interfaces.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAllAppointmentsForExpertPanelService _getAllAppointmentsForExpertPanel;
        private readonly IGetAppointmentDetailsForExpertPanelService _getAppointmentDetailsForExpertPanelService;

        public AppointmentsController(IGetAllAppointmentsForExpertPanelService getAllAppointmentsForExpertPanel
        ,IGetAppointmentDetailsForExpertPanelService getAppointmentDetailsForExpertPanelService)
        {
            _getAllAppointmentsForExpertPanel = getAllAppointmentsForExpertPanel;
            _getAppointmentDetailsForExpertPanelService = getAppointmentDetailsForExpertPanelService;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getAllAppointmentsForExpertPanel.Execute(expertId, Page, PageSize).Data;

            return View(model);
        }

        [HttpPost]
        public AppointmentDetailsForExpertPanel GetDetails(long id)
        {

            return _getAppointmentDetailsForExpertPanelService.Execute(id).Data;
        }
    }
}
