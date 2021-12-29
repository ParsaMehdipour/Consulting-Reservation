using CR.Common.Utilities;
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

        public AppointmentsController(IGetAllAppointmentsForExpertPanelService getAllAppointmentsForExpertPanel)
        {
            _getAllAppointmentsForExpertPanel = getAllAppointmentsForExpertPanel;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getAllAppointmentsForExpertPanel.Execute(expertId, Page, PageSize).Data;

            return View(model);
        }
    }
}
