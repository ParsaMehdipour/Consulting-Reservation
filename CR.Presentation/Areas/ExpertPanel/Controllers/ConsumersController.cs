using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.Consumers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class ConsumersController : Controller
    {
        private readonly IGetConsumersForExpertPanelService _getConsumersForExpertPanelService;
        private readonly IGetConsumerAppointmentsForExpertPanelService _getConsumerAppointmentsForExpertPanelService;

        public ConsumersController(IGetConsumersForExpertPanelService getConsumersForExpertPanelService
        ,IGetConsumerAppointmentsForExpertPanelService getConsumerAppointmentsForExpertPanelService)
        {
            _getConsumersForExpertPanelService = getConsumersForExpertPanelService;
            _getConsumerAppointmentsForExpertPanelService = getConsumerAppointmentsForExpertPanelService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumersForExpertPanelService.Execute(expertId,page, pageSize).Data;

            return View(model);
        }

        public IActionResult ConsumerDetails(long consumerId, int page = 1, int pageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerAppointmentsForExpertPanelService.Execute(expertId, consumerId, page, pageSize).Data;

            return View(model);
        }
    }
}
