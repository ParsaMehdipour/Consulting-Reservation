using CR.Core.DTOs.Appointments;
using CR.Core.Services.Interfaces.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.View
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class AppointmentsController : Controller
    {
        private readonly IGetAppointmentDetailsForConsumerPanelService _getAppointmentDetailsForConsumerPanelService;

        public AppointmentsController(IGetAppointmentDetailsForConsumerPanelService getAppointmentDetailsForConsumerPanelService)
        {
            _getAppointmentDetailsForConsumerPanelService = getAppointmentDetailsForConsumerPanelService;
        }

        [HttpPost]
        public AppointmentDetailsForExpertPanel GetDetails(long id)
        {
            return _getAppointmentDetailsForConsumerPanelService.Execute(id).Data;
        }
    }
}
