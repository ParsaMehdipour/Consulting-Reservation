using CR.Core.Services.Interfaces.Timings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class TimingsController : Controller
    {
        private readonly IGetAllTimingsForAdminService _getAllTimingsForAdminService;

        public TimingsController(IGetAllTimingsForAdminService getAllTimingsForAdminService)
        {
            _getAllTimingsForAdminService = getAllTimingsForAdminService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = _getAllTimingsForAdminService.Execute(page, pageSize).Data;

            return View(model);
        }
    }
}
