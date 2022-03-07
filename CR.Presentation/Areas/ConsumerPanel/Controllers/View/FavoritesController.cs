using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Favorites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class FavoritesController : Controller
    {
        private readonly IGetConsumerFavoritesService _getConsumerFavoritesService;

        public FavoritesController(IGetConsumerFavoritesService getConsumerFavoritesService)
        {
            _getConsumerFavoritesService = getConsumerFavoritesService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerFavoritesService.Execute(userId, page, pageSize).Data;

            return View(model);
        }
    }
}
