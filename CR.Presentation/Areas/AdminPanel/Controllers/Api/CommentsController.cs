using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IChangeCommentStatusService _changeCommentStatusService;

        public CommentsController(IChangeCommentStatusService changeCommentStatusService)
        {
            _changeCommentStatusService = changeCommentStatusService;
        }

        [Route("/api/Comments/ChangeStatus")]
        [HttpPost]
        public IActionResult ChangeStatus(RequestChangeCommentStatusDto request)
        {
            var result = _changeCommentStatusService.Execute(request);

            return new JsonResult(result);
        }
    }
}
