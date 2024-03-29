﻿using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class FinancialTransactionsController : Controller
    {
        private readonly IGetFinancialTransactionsForAdminService _getFinancialTransactionsForAdminService;

        public FinancialTransactionsController(IGetFinancialTransactionsForAdminService getFinancialTransactionsForAdminService)
        {
            _getFinancialTransactionsForAdminService = getFinancialTransactionsForAdminService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.FinancialTransactions;

            var model = _getFinancialTransactionsForAdminService.Execute(page, pageSize).Data;

            return View(model);
        }
    }
}
