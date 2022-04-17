using CR.Core.DTOs.RequestDTOs.Settings;
using CR.Core.Services.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingServices _settingServices;

        public SettingsController(ISettingServices settingServices)
        {
            _settingServices = settingServices;
        }

        [Route("/api/Settings/Add")]
        [HttpPost]
        public IActionResult Add([FromForm] AddSettingDto model)
        {
            var result = _settingServices.Add(model);

            return new JsonResult(result);
        }

        [Route("/api/Settings/Edit")]
        [HttpPost]
        public IActionResult Edit([FromForm] EditSettingDto model)
        {
            var result = _settingServices.Edit(model);

            return new JsonResult(result);
        }
    }
}
