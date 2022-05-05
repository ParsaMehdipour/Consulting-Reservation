using CR.Core.Services.Interfaces.Links;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.ViewComponents
{
    public class Links : ViewComponent
    {
        private readonly ILinkServices _linkServices;

        public Links(ILinkServices linkServices)
        {
            _linkServices = linkServices;
        }

        public IViewComponentResult Invoke()
        {
            var model = _linkServices.GetAllLinksForSite().Data;

            return View(model);
        }
    }
}
