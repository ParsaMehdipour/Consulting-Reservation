using System.Threading.Tasks;
using CR.Common.Utilities;
using CR.Core.DTOs.Account;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
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

            return View("Index",request);
        }
    }
}
