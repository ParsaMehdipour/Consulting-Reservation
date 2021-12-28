using System.Threading.Tasks;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.Users;
using CR.Core.Services.Impl.Users;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class ConsumersController : Controller
    {
        private readonly IGetAllConsumersService _getAllConsumersService;
        private readonly IRegisterConsumerFromAdminService _registerConsumerFromAdminService;
        private readonly IGetConsumerDetailsForAdminService _getConsumerDetailsForAdminService;
        private readonly IEditConsumerDetailsFromAdminService _editConsumerDetailsFromAdminService;

        public ConsumersController(IGetAllConsumersService getAllConsumersService
        ,IRegisterConsumerFromAdminService registerConsumerFromAdminService
        ,IGetConsumerDetailsForAdminService getConsumerDetailsForAdminService
        ,IEditConsumerDetailsFromAdminService editConsumerDetailsFromAdminService)
        {
            _getAllConsumersService = getAllConsumersService;
            _registerConsumerFromAdminService = registerConsumerFromAdminService;
            _getConsumerDetailsForAdminService = getConsumerDetailsForAdminService;
            _editConsumerDetailsFromAdminService = editConsumerDetailsFromAdminService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = _getAllConsumersService.Execute(page,pageSize).Data;

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewConsumer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewConsumer(RegisterConsumerFromAdminDto request)
        {
            var result = await _registerConsumerFromAdminService.Execute(request);

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult ConsumerDetails(long id)
        {
            var model = _getConsumerDetailsForAdminService.Execute(id).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditConsumerDetails(ConsumerDetailsForAdminDto request)
        {
            var result = _editConsumerDetailsFromAdminService.Execute(request);

            return new JsonResult(result);
        }
    }
}
