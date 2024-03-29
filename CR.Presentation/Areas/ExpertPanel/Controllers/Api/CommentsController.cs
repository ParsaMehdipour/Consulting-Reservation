﻿using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IAddNewReplyService _addNewReplyService;
        private readonly IAddNewCommentService _addNewCommentService;

        public CommentsController(IAddNewReplyService addNewReplyService
        , IAddNewCommentService addNewCommentService)
        {
            _addNewReplyService = addNewReplyService;
            _addNewCommentService = addNewCommentService;
        }

        [Route("/api/Comments/Reply")]
        [HttpPost]
        public IActionResult Reply([FromBody] RequestAddNewReplyDto request)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var result = _addNewReplyService.Execute(request, userId);

            return new JsonResult(result);
        }

        [Route("/api/Comments/AddNewCommentFromExpert")]
        [HttpPost]
        public IActionResult AddNewComments([FromBody] RequestAddNewCommentDto request)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var result = _addNewCommentService.Execute(request, userId);

            return new JsonResult(result);

        }
    }
}
