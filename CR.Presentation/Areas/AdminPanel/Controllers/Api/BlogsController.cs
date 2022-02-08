using CR.Core.DTOs.RequestDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IAddNewBlogService _addNewBlogService;

        public BlogsController(IAddNewBlogService addNewBlogService)
        {
            _addNewBlogService = addNewBlogService;
        }

        [Route("/api/Blogs/AddNewBlog")]
        [HttpPost]
        public IActionResult AddNewBlog([FromForm] RequestAddNewBlogDto model)
        {
            var result = _addNewBlogService.Execute(model);

            return new JsonResult(result);
        }
    }
}
