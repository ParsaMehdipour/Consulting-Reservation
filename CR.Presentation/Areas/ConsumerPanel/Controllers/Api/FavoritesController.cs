using CR.Core.DTOs.RequestDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IRemoveFromFavoritesListService _removeFromFavoritesListService;

        public FavoritesController(IRemoveFromFavoritesListService removeFromFavoritesListService)
        {
            _removeFromFavoritesListService = removeFromFavoritesListService;
        }

        [Route("/api/Favorites/RemoveFavorite")]
        [HttpPost]
        public IActionResult RemoveFavorite([FromForm] RequestRemoveFormFavoritesListDto request)
        {
            var result = _removeFromFavoritesListService.Execute(request);

            return new JsonResult(result);
        }
    }
}
