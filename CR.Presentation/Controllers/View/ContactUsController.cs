using CR.Core.Services.Interfaces.ContactUs;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class ContactUsController : Controller
    {
        private readonly IGetContactUsContentService _getContactUsContentService;

        public ContactUsController(IGetContactUsContentService getContactUsContentService)
        {
            _getContactUsContentService = getContactUsContentService;
        }

        public IActionResult Index()
        {
            var model = new ContactUsViewModel()
            {
                ContactUsContentDto = _getContactUsContentService.Execute().Data
            };

            return View(model);
        }
    }
}
