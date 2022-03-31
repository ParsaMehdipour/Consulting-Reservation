using CR.Core.Services.Interfaces.Blogs;
using CR.Core.Services.Interfaces.Comments;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class HomeController : Controller
    {
        private readonly IGetSpecialitiesForPresentationService _getSpecialitiesForPresentationService;
        private readonly IGetExpertsForPresentationService _getExpertsForPresentationService;
        private readonly IGetLatestBlogsForSiteService _getLatestBlogsForSiteService;
        private readonly IGetCommentsForMainViewService _getCommentsForMainViewService;

        public HomeController(IGetSpecialitiesForPresentationService getSpecialitiesForPresentationService
        , IGetExpertsForPresentationService getExpertsForPresentationService
        , IGetLatestBlogsForSiteService getLatestBlogsForSiteService
        , IGetCommentsForMainViewService getCommentsForMainViewService)
        {
            _getSpecialitiesForPresentationService = getSpecialitiesForPresentationService;
            _getExpertsForPresentationService = getExpertsForPresentationService;
            _getLatestBlogsForSiteService = getLatestBlogsForSiteService;
            _getCommentsForMainViewService = getCommentsForMainViewService;
        }

        public IActionResult Index()
        {
            var viewModel = new PresentationViewModel()
            {
                SpecialityDtos = _getSpecialitiesForPresentationService.Execute().Data,
                ExpertForPresentationDtos = _getExpertsForPresentationService.Execute().Data,
                BlogForPresentationDtos = _getLatestBlogsForSiteService.Execute().Data,
                CommentForMainViewDtos = _getCommentsForMainViewService.Execute().Data
            };

            return View(viewModel);
        }

        //public async Task SendSMS()
        //{
        //    var model = new SMSModel()
        //    {
        //        toNum = "09122502978",
        //        patternCode = SMSPatterns.VerificationCodePatternCode,
        //        inputData = new List<Dictionary<string, string>>()
        //        {
        //            new Dictionary<string, string> { { SMSInputs.VerificationCode, "123456789" } },
        //        },
        //    };

        //    var uri = "https://ippanel.com/api/select";

        //    var result = await CallApi<object>(uri, model);

        //}

    }
}
