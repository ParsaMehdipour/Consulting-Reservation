using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CR.Presentation.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IAddAppointmentService _addAppointmentService;
        private readonly IGetUserFlagService _getUserFlagService;
        private ResultCheckUserFlagService resultCheckUserFlag;

        public CheckoutController(IAddAppointmentService addAppointmentService
        , IHttpContextAccessor contextAccessor
        , IGetUserFlagService _getUserFlagService)
        {
            _addAppointmentService = addAppointmentService;
            this._getUserFlagService = _getUserFlagService;

            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            resultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        [HttpPost]
        public IActionResult Index(List<RequestAddAppointmentDto> requests)
        {
            if (resultCheckUserFlag.UserFlag != UserFlag.Consumer)
                return new JsonResult(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تنها مراجعه کنندگان قابلیت رزرو وقت دارند"
                });

            var consumerId = ClaimUtility.GetUserId(User).Value;

            var result = _addAppointmentService.Execute(requests, consumerId);

            return new JsonResult(result);
        }

        [Route("/checkout/successful")]
        public IActionResult CheckoutSuccessful()
        {
            return View();
        }
    }
}
