using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class BlogsController : Controller
    {
        private readonly IGetBlogDetailsService _getBlogDetailsService;

        public BlogsController(IGetBlogDetailsService getBlogDetailsService)
        {
            _getBlogDetailsService = getBlogDetailsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBlogDetails(long id)
        {

        }
    }
}
