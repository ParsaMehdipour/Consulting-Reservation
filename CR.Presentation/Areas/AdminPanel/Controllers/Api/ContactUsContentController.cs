using CR.Core.DTOs.RequestDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class ContactUsContentController : ControllerBase
    {
        private readonly IAddContactUsContentService _addContactUsContentService;
        private readonly IEditContactUsContentService _editContactUsContentService;

        public ContactUsContentController(IAddContactUsContentService addContactUsContentService,
            IEditContactUsContentService editContactUsContentService)
        {
            _addContactUsContentService = addContactUsContentService;
            _editContactUsContentService = editContactUsContentService;
        }

        [Route("/api/ContactUsContent/Create")]
        [HttpPost]
        public IActionResult Create([FromForm] RequestAddNewContactUsContentDto request)
        {
            var result = _addContactUsContentService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/ContactUsContent/Edit")]
        [HttpPost]
        public IActionResult Edit([FromForm] RequestEditContactUsContentDto request)
        {
            var result = _editContactUsContentService.Execute(request);

            return new JsonResult(result);
        }

    }
}
