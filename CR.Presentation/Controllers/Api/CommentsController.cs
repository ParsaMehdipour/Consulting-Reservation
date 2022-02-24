using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IAddNewCommentService _addNewCommentService;
        private readonly SignInManager<User> _signInManager;

        public CommentsController(IAddNewCommentService addNewCommentService
            , SignInManager<User> signInManager)
        {
            _addNewCommentService = addNewCommentService;
            _signInManager = signInManager;
        }

        [Route("/api/Comments/AddNewComments")]
        [HttpPost]
        public IActionResult AddNewComments([FromBody] RequestAddNewCommentDto request)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = ClaimUtility.GetUserId(User).Value;

                var result = _addNewCommentService.Execute(request, userId);

                return new JsonResult(result);
            }

            return new JsonResult(new ResultDto()
            {
                IsSuccess = false,
                Message = "لطفا برای نظر دادن ابتدا وارد سایت شوید"
            });
        }
    }
}
