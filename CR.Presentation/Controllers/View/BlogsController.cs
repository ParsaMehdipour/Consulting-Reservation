using CR.Core.Services.Interfaces.Blogs;
using CR.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class BlogsController : Controller
    {
        private readonly IGetBlogDetailsForPresentationService _getBlogDetailsForPresentationService;
        private readonly IGetLatestBlogsForSiteService _getLatestBlogsForSiteService;

        public BlogsController(IGetBlogDetailsForPresentationService getBlogDetailsForPresentationService
        , IGetLatestBlogsForSiteService getLatestBlogsForSiteService)
        {
            _getBlogDetailsForPresentationService = getBlogDetailsForPresentationService;
            _getLatestBlogsForSiteService = getLatestBlogsForSiteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogDetails(string slug)
        {
            var model = new BlogDetailsViewModel()
            {
                BlogDetailsForPresentationDto = _getBlogDetailsForPresentationService.Execute(slug).Data,
                LatestBlogsDto = _getLatestBlogsForSiteService.Execute().Data
            };

            return View(model);
        }
    }
}
