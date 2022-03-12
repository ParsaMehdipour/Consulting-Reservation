using CR.Core.DTOs.RequestDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IAddNewContactUsService _addNewContactUsService;

        public ContactUsController(IAddNewContactUsService addNewContactUsService)
        {
            _addNewContactUsService = addNewContactUsService;
        }

        [Route("/api/ContactUs/Send")]
        [HttpPost]
        public IActionResult Send([FromForm] RequestAddNewContactUsDto request)
        {
            var result = _addNewContactUsService.Execute(request);

            return new JsonResult(result);
        }
    }
}
