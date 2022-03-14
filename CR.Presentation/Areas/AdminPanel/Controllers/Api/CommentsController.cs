using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IChangeCommentStatusService _changeCommentStatusService;
        private readonly IAddNewCommentService _addNewCommentService;

        public CommentsController(IChangeCommentStatusService changeCommentStatusService
        , IAddNewCommentService addNewCommentService)
        {
            _changeCommentStatusService = changeCommentStatusService;
            _addNewCommentService = addNewCommentService;
        }

        [Route("/api/Comments/ChangeStatus")]
        [HttpPost]
        public IActionResult ChangeStatus(RequestChangeCommentStatusDto request)
        {
            var result = _changeCommentStatusService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/Comments/AddNewCommentFromAdmin")]
        [HttpPost]
        public IActionResult AddNewComments([FromBody] RequestAddNewCommentDto request)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var result = _addNewCommentService.Execute(request, userId);

            return new JsonResult(result);

        }
    }
}
