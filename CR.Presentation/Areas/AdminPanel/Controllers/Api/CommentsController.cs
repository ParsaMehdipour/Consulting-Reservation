using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.Core.Services.Interfaces.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IChangeCommentStatusService _changeCommentStatusService;
        private readonly IAddNewCommentService _addNewCommentService;
        private readonly IGetCommentDetailsForAdminPanelService _getCommentDetailsForAdminPanelService;
        private readonly IShowCommentInMainPageService _showCommentInMainPageService;

        public CommentsController(IChangeCommentStatusService changeCommentStatusService
        , IAddNewCommentService addNewCommentService
        , IGetCommentDetailsForAdminPanelService getCommentDetailsForAdminPanelService
        , IShowCommentInMainPageService showCommentInMainPageService)
        {
            _changeCommentStatusService = changeCommentStatusService;
            _addNewCommentService = addNewCommentService;
            _getCommentDetailsForAdminPanelService = getCommentDetailsForAdminPanelService;
            _showCommentInMainPageService = showCommentInMainPageService;
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

        [Route("/api/Comments/ChangeShowStatus")]
        [HttpPost]
        public IActionResult ChangeShowStatus([FromForm] RequestChangeCommentShowStatusDto request)
        {
            var result = _showCommentInMainPageService.Execute(request.commentId, request.showStatus);

            return new JsonResult(result);
        }

        //[Route("/api/Comments/GetDetails")]
        //[HttpPost]
        //public IActionResult GetDetails([FromBody] RequestGetCommentDetailsDto request)
        //{
        //    var result = _getCommentDetailsForAdminPanelService.Execute(request.id).Data;

        //    return new JsonResult(result);
        //}
    }
}
