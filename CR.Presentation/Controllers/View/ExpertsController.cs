using CR.Common.Convertor;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Enums;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class ExpertsController : Controller
    {
        private readonly IGetExpertsForSiteService _getExpertsForSiteService;
        private readonly IGetExpertDetailsForSiteService _getExpertDetailsForSiteService;
        private readonly IGetSpecialitiesForSearchService _getSpecialitiesForSearchService;
        private readonly IGetExpertDetailsForReservationService _getExpertDetailsForReservationService;
        private readonly IGetThisDateExpertDetailsForReservationService _getThisDateExpertDetailsForReservationService;

        public ExpertsController(IGetExpertsForSiteService getExpertsForSiteService
        , IGetExpertDetailsForSiteService getExpertDetailsForSiteService
        , IGetSpecialitiesForSearchService getSpecialitiesForSearchService
        , IGetExpertDetailsForReservationService getExpertDetailsForReservationService
        ,IGetThisDateExpertDetailsForReservationService getThisDateExpertDetailsForReservationService)
        {
            _getExpertsForSiteService = getExpertsForSiteService;
            _getExpertDetailsForSiteService = getExpertDetailsForSiteService;
            _getSpecialitiesForSearchService = getSpecialitiesForSearchService;
            _getExpertDetailsForReservationService = getExpertDetailsForReservationService;
            _getThisDateExpertDetailsForReservationService = getThisDateExpertDetailsForReservationService;
        }

        public IActionResult Index(string searchKey, string speciality, GenderType gender, int page = 1, int pageSize = 20)
        {
            var searchModel = new SearchViewModel()
            {
                ResultGetExpertsForSiteDto = _getExpertsForSiteService.Execute(searchKey, speciality, gender, page, pageSize).Data,
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

        [Authorize]
        [HttpPost]
        public IActionResult Reservation(long expertInformationId,string passedDate)
        {
            var date = passedDate.ToGeorgianDateTime();

            var model = _getThisDateExpertDetailsForReservationService.Execute(expertInformationId, date).Data;
            return View(model);
        }
    }
}
