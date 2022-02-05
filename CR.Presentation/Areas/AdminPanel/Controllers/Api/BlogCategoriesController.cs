using CR.Core.DTOs.RequestDTOs.BlogCategories;
using CR.Core.Services.Interfaces.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class BlogCategoriesController : ControllerBase
    {
        private readonly IDeleteBlogCategoryService _deleteBlogCategoryService;

        public BlogCategoriesController(IDeleteBlogCategoryService deleteBlogCategoryService)
        {
            _deleteBlogCategoryService = deleteBlogCategoryService;
        }

        [Route("/api/BlogCategories/DeleteBlogCategory")]
        [HttpPost]
        public IActionResult DeleteBlogCategory(RequestDeleteBlogCategory model)
        {
            var result = _deleteBlogCategoryService.Execute(model.id);

            return new JsonResult(result);
        }
    }
}
