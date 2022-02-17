using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Blogs;
using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IAddNewBlogService _addNewBlogService;
        private readonly IDeleteBlogService _deleteBlogService;
        private readonly IEditBlogFromAdminService _editBlogFromAdminService;

        public BlogsController(IAddNewBlogService addNewBlogService
        , IDeleteBlogService deleteBlogService
        , IEditBlogFromAdminService editBlogFromAdminService)
        {
            _addNewBlogService = addNewBlogService;
            _deleteBlogService = deleteBlogService;
            _editBlogFromAdminService = editBlogFromAdminService;
        }

        [Route("/api/Blogs/AddNewBlog")]
        [HttpPost]
        public IActionResult AddNewBlog([FromForm] RequestAddNewBlogDto model)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var result = _addNewBlogService.Execute(model, userId);

            return new JsonResult(result);
        }

        [Route("/api/Blogs/RemoveBlog")]
        [HttpPost]
        public IActionResult RemoveBlog(RequestDeleteBlogDto request)
        {
            var result = _deleteBlogService.Execute(request.id);

            return new JsonResult(result);
        }

        [Route("/api/Blogs/EditBlogDetails")]
        [HttpPost]
        public IActionResult EditBlog([FromForm] RequestEditBlogDto request)
        {
            var result = _editBlogFromAdminService.Execute(request);

            return new JsonResult(result);
        }
    }
}
