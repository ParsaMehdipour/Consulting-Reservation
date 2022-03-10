using CR.Core.DTOs.RequestDTOs.AboutUs;
using CR.Core.Services.Interfaces.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IAddAboutUsService _addAboutUsService;
        private readonly IEditAboutUsService _editAboutUsService;

        public AboutUsController(IAddAboutUsService addAboutUsService
        , IEditAboutUsService editAboutUsService)
        {
            _addAboutUsService = addAboutUsService;
            _editAboutUsService = editAboutUsService;
        }

        [Route("/api/AboutUs/Create")]
        [HttpPost]
        public IActionResult Create([FromForm] RequestAddAboutUsDto request)
        {
            var result = _addAboutUsService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/AboutUs/Edit")]
        [HttpPost]
        public IActionResult Edit([FromForm] RequestEditAboutUsDto request)
        {
            var result = _editAboutUsService.Execute(request);

            return new JsonResult(result);
        }
    }
}
