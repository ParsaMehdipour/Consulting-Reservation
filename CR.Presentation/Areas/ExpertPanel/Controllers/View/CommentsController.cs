using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.View
{
    [Area("ExpertPanel")]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IGetExpertCommentsForExpertPanelService _getExpertCommentsForExpertPanelService;

        public CommentsController(IGetExpertCommentsForExpertPanelService getExpertCommentsForExpertPanelService)
        {
            _getExpertCommentsForExpertPanelService = getExpertCommentsForExpertPanelService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getExpertCommentsForExpertPanelService.Execute(expertId, page, pageSize).Data;

            return View(model);
        }
    }
}
