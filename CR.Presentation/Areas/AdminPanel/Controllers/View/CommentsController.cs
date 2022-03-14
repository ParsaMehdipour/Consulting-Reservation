using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class CommentsController : Controller
    {
        private readonly IGetExpertCommentsForAdminPanelService _getExpertCommentsForAdminPanelService;
        private readonly IGetCommentDetailsForAdminPanelService _getCommentDetailsForAdminPanelService;

        public CommentsController(IGetExpertCommentsForAdminPanelService getExpertCommentsForAdminPanelService
        , IGetCommentDetailsForAdminPanelService getCommentDetailsForAdminPanelService)
        {
            _getExpertCommentsForAdminPanelService = getExpertCommentsForAdminPanelService;
            _getCommentDetailsForAdminPanelService = getCommentDetailsForAdminPanelService;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.ExpertComments;

            var model = _getExpertCommentsForAdminPanelService.Execute(Page, PageSize).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult GetDetails(long id)
        {
            var result = _getCommentDetailsForAdminPanelService.Execute(id).Data;

            return new JsonResult(result);
        }
    }
}
