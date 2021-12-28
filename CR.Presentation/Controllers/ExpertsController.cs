using System;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Enums;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly IGetExpertsForSiteService _getExpertsForSiteService;
        private readonly IGetExpertDetailsForSiteService _getExpertDetailsForSiteService;
        private readonly IGetSpecialitiesForSearchService _getSpecialitiesForSearchService;
        private readonly IGetExpertDetailsForReservationService _getExpertDetailsForReservationService;

        public ExpertsController(IGetExpertsForSiteService getExpertsForSiteService
        ,IGetExpertDetailsForSiteService getExpertDetailsForSiteService
        ,IGetSpecialitiesForSearchService getSpecialitiesForSearchService
        ,IGetExpertDetailsForReservationService getExpertDetailsForReservationService)
        {
            _getExpertsForSiteService = getExpertsForSiteService;
            _getExpertDetailsForSiteService = getExpertDetailsForSiteService;
            _getSpecialitiesForSearchService = getSpecialitiesForSearchService;
            _getExpertDetailsForReservationService = getExpertDetailsForReservationService;
        }

        public IActionResult Index(string searchKey, string speciality, string location,GenderType gender, int page = 1, int pageSize = 20)
        {
            var searchModel = new SearchViewModel()
            {
                ResultGetExpertsForSiteDto = _getExpertsForSiteService.Execute(searchKey, speciality, location, gender, page, pageSize).Data,
                Specialities = _getSpecialitiesForSearchService.Execute()
            };

            return View(searchModel);
        }

        [HttpGet]
        public IActionResult ExpertDetails(long expertInformationId)
        {
            var model = _getExpertDetailsForSiteService.Execute(expertInformationId).Data;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Reservation(long expertInformationId)
        {
            var model = _getExpertDetailsForReservationService.Execute(expertInformationId).Data;

            return View(model);
        }

        public void Search(GenderType gender)
        {
            Console.WriteLine();
        }
    }
}
