using CR.Core.DTOs.Appointment;
using CR.Core.Services.Interfaces.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAppointmentDetailsForExpertPanelService _getAppointmentDetailsForExpertPanelService;

        public AppointmentsController(IGetAppointmentDetailsForExpertPanelService getAppointmentDetailsForExpertPanelService)
        {
            _getAppointmentDetailsForExpertPanelService = getAppointmentDetailsForExpertPanelService;
        }

        [HttpPost]
        public AppointmentDetailsForExpertPanel GetDetails(long id)
        {
            return _getAppointmentDetailsForExpertPanelService.Execute(id).Data;
        }
    }
}
