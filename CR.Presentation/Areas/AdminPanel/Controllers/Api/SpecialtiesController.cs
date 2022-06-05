using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.RequestDTOs.Specialty;
using CR.Core.Services.Interfaces.Specialites;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly IGetAllSpecialitiesService _getAllSpecialitiesService;

        public SpecialtiesController(IGetAllSpecialitiesService getAllSpecialitiesService)
        {
            _getAllSpecialitiesService = getAllSpecialitiesService;
        }

        [HttpPost]
        [Route("/api/SpecialtiesController/AddNewSpecialty")]
        public IActionResult AddNewSpecialty([FromForm] RequestAddNewSpecialtyDto request)
        {
            var result = _getAllSpecialitiesService.AddNewSpecialty(request);

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("/api/SpecialtiesController/DeleteSpecialty")]
        public IActionResult DeleteSpecialty(RequestDeleteSpecialtyDto request)
        {
            var result = _getAllSpecialitiesService.DeleteSpecialty(request.id);

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("/api/SpecialtiesController/EditSpecialty")]
        public IActionResult EditSpecialty([FromForm] RequestEditSpecialityDto request)
        {
            var result = _getAllSpecialitiesService.EditSpecialty(request);

            return new JsonResult(result);
        }
    }
}
