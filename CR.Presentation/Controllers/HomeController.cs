using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetSpecialitiesForPresentationService _getSpecialitiesForPresentationService;
        private readonly IGetExpertsForPresentationService _getExpertsForPresentationService;

        public HomeController(IGetSpecialitiesForPresentationService getSpecialitiesForPresentationService
        ,IGetExpertsForPresentationService getExpertsForPresentationService)
        {
            _getSpecialitiesForPresentationService = getSpecialitiesForPresentationService;
            _getExpertsForPresentationService = getExpertsForPresentationService;
        }

        public IActionResult Index()
        {
            var viewModel = new PresentationViewModel()
            {
                SpecialityDtos = _getSpecialitiesForPresentationService.Execute().Data,
                ExpertForPresentationDtos = _getExpertsForPresentationService.Execute().Data
            };

            return View(viewModel);
        }
    }
}
