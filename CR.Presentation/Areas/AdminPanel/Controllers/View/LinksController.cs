﻿using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.Links;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    public class LinksController : Controller
    {
        private readonly ILinkServices _linkServices;

        public LinksController(ILinkServices linkServices)
        {
            _linkServices = linkServices;
        }

        public IActionResult Index(string searchKey, int Page = 1, int PageSize = 20, int ParentId = 0)
        {
            TempData["activemenu"] = ActiveMenu.Links;

            var model = _linkServices.GetAllLinksForAdminPanel(searchKey, Page, PageSize, ParentId).Data;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(long? parentId)
        {
            TempData["activemenu"] = ActiveMenu.Links;

            ViewBag.parentId = parentId;

            return View();
        }

        [HttpGet]
        public IActionResult Edit(long linkId)
        {
            TempData["activemenu"] = ActiveMenu.Links;

            var model = _linkServices.GetLink(linkId).Data;

            return View(model);
        }
    }
}