using CR.Core.DTOs.RequestDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IAddNewFavoriteService _addNewFavoriteService;

        public ExpertsController(IAddNewFavoriteService addNewFavoriteService)
        {
            _addNewFavoriteService = addNewFavoriteService;
        }

        [Route("/api/Experts/AddToFavorites")]
        [HttpPost]
        public IActionResult AddToFavorites([FromForm] RequestAddNewFavoriteDto request)
        {
            var result = _addNewFavoriteService.Execute(request);

            return new JsonResult(result);
        }

    }
}
