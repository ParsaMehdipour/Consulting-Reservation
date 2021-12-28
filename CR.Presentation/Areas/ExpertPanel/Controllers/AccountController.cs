using System.Threading.Tasks;
using CR.Common.Utilities;
using CR.Core.DTOs.Account;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers
{
    [Authorize]
    [Area("ExpertPanel")]
    public class AccountController : Controller
    {
        private readonly IGetUserFlagService _getUserFlagService;
        private readonly UserManager<User> _userManager;
        public ResultCheckUserFlagService ResultCheckUserFlag { get; set; }

        public AccountController(IHttpContextAccessor contextAccessor
            ,IGetUserFlagService _getUserFlagService
            , UserManager<User> userManager)
        {
            this._getUserFlagService = _getUserFlagService;
            _userManager = userManager;


            var userId = ClaimUtility.GetUserId(contextAccessor.HttpContext?.User);

            ResultCheckUserFlag = _getUserFlagService.Execute(userId);
        }

        public IActionResult Index()
        {
            if (ResultCheckUserFlag.UserFlag != UserFlag.Expert)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            if (ModelState.IsValid)
            {
                request.UserId = ClaimUtility.GetUserId(User).Value;

                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                var result = await _userManager.ChangePasswordAsync(user, request.oldPassword, request.newPassword);

                if (result.Succeeded)
                {
                    ViewData["ErrorMessage"] = "رمزعبور شما با موفقیت تغییر یافت";
                    return View("Index", request);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Index", request);
        }
    }
}
