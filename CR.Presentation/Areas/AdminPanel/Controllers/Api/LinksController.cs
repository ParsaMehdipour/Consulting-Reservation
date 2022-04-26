using CR.Core.DTOs.RequestDTOs.Links;
using CR.Core.Services.Interfaces.Links;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkServices _linkServices;

        public LinksController(ILinkServices linkServices)
        {
            _linkServices = linkServices;
        }

        [HttpPost]
        [Route("/api/LinksController/AddNewLink")]
        public IActionResult AddNewLink([FromForm] RequestAddNewLinkDto request)
        {
            var result = _linkServices.AddNewLink(request);

            return new JsonResult(result);
        }
    }
}
