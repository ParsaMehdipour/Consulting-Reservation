using CR.Core.Services.Interfaces.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IAddAboutUsService _addAboutUsService;
        private readonly IEditAboutUsService _editAboutUsService;

        public ExpertsController(IAddAboutUsService addAboutUsService, IEditAboutUsService editAboutUsService)
        {
            _addAboutUsService = addAboutUsService;
            _editAboutUsService = editAboutUsService;
        }

        [HttpGet]
        [Route("/api/Experts/select2")]
        public IActionResult Select2(string search, int page)
        {
            //Select2Request model = new Select2Request { search = search, page = page };

            //var results = attributeTypeService.GetAllAttributeTypeForSelect2(model);

            return new JsonResult("");
        }
    }
}
