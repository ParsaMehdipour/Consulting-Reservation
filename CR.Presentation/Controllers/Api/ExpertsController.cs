using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IAddNewFavoriteService _addNewFavoriteService;
        private readonly SignInManager<User> _signInManager;

        public ExpertsController(IAddNewFavoriteService addNewFavoriteService
        , SignInManager<User> signInManager)
        {
            _addNewFavoriteService = addNewFavoriteService;
            _signInManager = signInManager;
        }

        [Route("/api/Experts/AddToFavorites")]
        [HttpPost]
        public IActionResult AddToFavorites([FromForm] RequestAddNewFavoriteDto request)
        {

            if (_signInManager.IsSignedIn(User))
            {
                var result = _addNewFavoriteService.Execute(request);

                return new JsonResult(result);
            }
            else
            {
                return new JsonResult(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا ابتدا وارد سایت شوید"
                });
            }

        }

    }
}
