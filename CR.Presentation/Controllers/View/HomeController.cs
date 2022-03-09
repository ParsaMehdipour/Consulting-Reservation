using CR.Core.Services.Interfaces.Blogs;
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

        public HomeController(IGetSpecialitiesForPresentationService getSpecialitiesForPresentationService
        , IGetExpertsForPresentationService getExpertsForPresentationService
        , IGetLatestBlogsForSiteService getLatestBlogsForSiteService)
        {
            _getSpecialitiesForPresentationService = getSpecialitiesForPresentationService;
            _getExpertsForPresentationService = getExpertsForPresentationService;
            _getLatestBlogsForSiteService = getLatestBlogsForSiteService;
        }

        public IActionResult Index()
        {
            var viewModel = new PresentationViewModel()
            {
                SpecialityDtos = _getSpecialitiesForPresentationService.Execute().Data,
                ExpertForPresentationDtos = _getExpertsForPresentationService.Execute().Data,
                BlogForPresentationDtos = _getLatestBlogsForSiteService.Execute().Data
            };

            return View(viewModel);
        }

    }
}
