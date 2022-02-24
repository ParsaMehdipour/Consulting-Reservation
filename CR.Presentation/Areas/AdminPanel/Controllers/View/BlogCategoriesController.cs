using System.Linq;
using CR.Common.ActiveMenus;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.BlogCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Authorize]
    [Area("AdminPanel")]
    public class BlogCategoriesController : Controller
    {
        private readonly IGetBlogCategoriesForAdminPanelService _getBlogCategoriesForAdminPanelService;
        private readonly IAddNewBlogCategoryService _addNewBlogCategoryService;
        private readonly IGetBlogCategoryDetailsService _getBlogCategoryDetailsService;
        private readonly IEditBlogCategoryDetailsService _editBlogCategoryDetailsService;

        public BlogCategoriesController(IGetBlogCategoriesForAdminPanelService getBlogCategoriesForAdminPanelService
        , IAddNewBlogCategoryService addNewBlogCategoryService
        , IGetBlogCategoryDetailsService getBlogCategoryDetailsService
        , IEditBlogCategoryDetailsService editBlogCategoryDetailsService)
        {
            _getBlogCategoriesForAdminPanelService = getBlogCategoriesForAdminPanelService;
            _addNewBlogCategoryService = addNewBlogCategoryService;
            _getBlogCategoryDetailsService = getBlogCategoryDetailsService;
            _editBlogCategoryDetailsService = editBlogCategoryDetailsService;
        }

        public IActionResult Index(long? parentId, int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.BlogCategories;

            var model = _getBlogCategoriesForAdminPanelService.Execute(parentId, page, pageSize).Data.BlogCategoryForAdminPanelDtos;

            ViewData["parentId"] = parentId;

            var parentBlogCategory = model.Select(p => new
            {
                p.Id,
                p.Name
            });

            ViewBag.parent = new SelectList(parentBlogCategory, "Id", "Name");

            return View(_getBlogCategoriesForAdminPanelService.Execute(parentId).Data);
        }

        public IActionResult AddNewBlogCategory(long? parentId)
        {
            TempData["activemenu"] = ActiveMenu.BlogCategories;

            ViewBag.parentId = parentId;

            return View();
        }

        [HttpPost]
        public IActionResult AddNewBlogCategory(RequestAddNewBlogCategoryDto model)
        {
            var result = _addNewBlogCategoryService.Execute(model);

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            TempData["activemenu"] = ActiveMenu.BlogCategories;

            var details = _getBlogCategoryDetailsService.Execute(id).Data;

            return View(details);
        }

        [HttpPost]
        public IActionResult EditBlogCategory(RequestEditBlogCategoryDto request)
        {
            var result = _editBlogCategoryDetailsService.Execute(request);

            return new JsonResult(result);
        }

    }
}
