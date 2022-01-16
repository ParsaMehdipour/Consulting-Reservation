using CR.Core.DTOs.CommissionAndDiscounts;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.CommissionAndDiscounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class CommissionAndDiscountsController : Controller
    {
        private readonly IGetAllCommissionAndDiscountsForAdminService _getAllCommissionAndDiscountsForAdminService;
        private readonly IAddNewCommissionAndDiscountService _addNewCommissionAndDiscountService;
        private readonly IGetCommissionAndDiscountDetailsForAdminService _getCommissionAndDiscountDetailsForAdminService;
        private readonly IEditCommissionAndDiscountService _editCommissionAndDiscountService;

        public CommissionAndDiscountsController(IGetAllCommissionAndDiscountsForAdminService getAllCommissionAndDiscountsForAdminService
        ,IAddNewCommissionAndDiscountService addNewCommissionAndDiscountService
        ,IGetCommissionAndDiscountDetailsForAdminService getCommissionAndDiscountDetailsForAdminService
        ,IEditCommissionAndDiscountService editCommissionAndDiscountService)
        {
            _getAllCommissionAndDiscountsForAdminService = getAllCommissionAndDiscountsForAdminService;
            _addNewCommissionAndDiscountService = addNewCommissionAndDiscountService;
            _getCommissionAndDiscountDetailsForAdminService = getCommissionAndDiscountDetailsForAdminService;
            _editCommissionAndDiscountService = editCommissionAndDiscountService;
        }

        public IActionResult Index()
        {
            var model = _getAllCommissionAndDiscountsForAdminService.Execute().Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(RequestAddNewCommissionAndDiscountDto request)
        {
            var result = _addNewCommissionAndDiscountService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public CommissionAndDiscountDetailsForAdminDto GetDetails(long id)
        {
            return _getCommissionAndDiscountDetailsForAdminService.Execute(id).Data;
        }

        [HttpPost]
        public IActionResult Edit(RequestEditCommissionAndDiscountDto request)
        {
            var result = _editCommissionAndDiscountService.Execute(request);

            return new JsonResult(result);
        }
    }
}
