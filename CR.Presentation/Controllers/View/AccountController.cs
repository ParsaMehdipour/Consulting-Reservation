using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Common.Utilities.PhoneTotp;
using CR.Common.Utilities.PhoneTotp.Providers;
using CR.Core.DTOs.Account;
using CR.Core.DTOs.RequestDTOs.Account;
using CR.Core.DTOs.SMS;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.View
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRegisterAsExpertService _registerAsExpertService;
        private readonly PhoneTotpOptions _phoneTotpOptions;
        private readonly IPhoneTotpProvider _phoneTotpProvider;

        public AccountController(
            UserManager<User> userManager
            , SignInManager<User> signInManager
            , IRegisterAsExpertService registerAsExpertService
            , IOptions<PhoneTotpOptions> phoneTotpOptions
            , IPhoneTotpProvider phoneTotpProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerAsExpertService = registerAsExpertService;
            _phoneTotpOptions = phoneTotpOptions?.Value ?? new PhoneTotpOptions();
            _phoneTotpProvider = phoneTotpProvider;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ResultDto> SignupVerificationCode([FromForm] PhoneNumberForVerificationCodeDto model)
        {

            User user = await _userManager.FindByNameAsync(model.phoneNumber);

            if (user != null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "این شماره تماس در سیستم ثبت شده است لطفا وارد شوید"
                };
            }

            if (TempData.ContainsKey("PTCPhone"))
            {
                TempData.Remove("PTCPhone");
            }

            if (TempData.ContainsKey("PTC"))
            {
                var totpTempDataModel = JsonSerializer.Deserialize<PhoneTempDataModel>(TempData["PTC"].ToString()!);
                if (totpTempDataModel.ExpirationTime >= DateTime.Now)
                {
                    var differenceInSeconds = (int)(totpTempDataModel.ExpirationTime - DateTime.Now).TotalSeconds;
                    TempData.Keep("PTC");
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = $"برای ارسال دوباره کد، لطفا {differenceInSeconds} ثانیه صبر کنید."
                    };
                }
            }

            byte[] secretKey;
            using (var rng = new RNGCryptoServiceProvider())
            {
                secretKey = new byte[32];
                rng.GetBytes(secretKey);
            }

            var verificationCode = _phoneTotpProvider.GenerateTotp(secretKey);

            var smsModel = new SMSModel()
            {
                toNum = model.phoneNumber,
                patternCode = SMSPatterns.VerificationCodePatternCode,
                inputData = new List<Dictionary<string, string>>()
                {
                    new Dictionary<string, string>
                    {
                        {SMSInputs.VerificationCode, verificationCode },
                    },
                },
            };

            var uri = "https://ippanel.com/api/select";

            await CallApiSMSPanel<object>(uri, smsModel);

            TempData["PTCPhone"] = JsonSerializer.Serialize(new PhoneNumberTempDataModel()
            {
                PhoneNumber = model.phoneNumber,
                ExpirationTime = DateTime.Now.AddMinutes(10)
            });

            TempData["PTC"] = JsonSerializer.Serialize(new PhoneTempDataModel()
            {
                SecretKey = secretKey,
                ExpirationTime = DateTime.Now.AddSeconds(_phoneTotpOptions.StepInSeconds)
            });

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کد تایید به شماره شما ارسال شد"
            };

        }

        [HttpPost]
        public ResultDto Verify(VerificationCodeForVerifyDto model)
        {
            if (!TempData.ContainsKey("PTC"))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا دوباره درخواست ارسال کد تایید دهید"
                };
            }

            var totpTempDataModel = JsonSerializer.Deserialize<PhoneTempDataModel>(TempData["PTC"].ToString()!);
            if (totpTempDataModel.ExpirationTime <= DateTime.Now)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کد ارسال شده منقضی شده، لطفا کد جدیدی دریافت کنید."
                };
            }

            var result = _phoneTotpProvider.VerifyTotp(totpTempDataModel.SecretKey, model.verificationCode);

            if (result.Succeeded)
            {
                TempData.Remove("PTC");

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کد تایید با موفقیت پذیرفته شد"
                };
            }

            TempData.Keep();

            return new ResultDto()
            {
                IsSuccess = false,
                Message = "کد ارسال شده معتبر نمی باشد"
            };
        }

        [HttpPost]
        public async Task<ResultDto> ConfirmSignup([FromForm] PasswordForSignupConfirmationDto model)
        {
            if (TempData.ContainsKey("PTCPhone"))
            {
                var totpTempDataModel = JsonSerializer.Deserialize<PhoneNumberTempDataModel>(TempData["PTCPhone"].ToString()!);
                if (totpTempDataModel.ExpirationTime <= DateTime.Now)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا مجددا برای ثبت نام اقدام نمایید"
                    };
                }

                var user = new User()
                {
                    PhoneNumber = totpTempDataModel.PhoneNumber,
                    UserName = totpTempDataModel.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "ثبت نام با موفقیت انجام شد"
                    };
                }

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "ثبت نام با موفقیت انجام نشد لطفا مجددا تلاش نمایید"
                };

            }

            return new ResultDto()
            {
                IsSuccess = false,
                Message = "لطفا مجددا برای ثبت نام اقدام نمایید"
            };

        }

        [HttpGet]
        public IActionResult ExpertSignup()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExpertSignup(RegisterExpertViewModel model)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.PhoneNumber,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    var signedUser = await _userManager.FindByNameAsync(model.PhoneNumber);

                    _registerAsExpertService.Execute(signedUser.Id, model.FirstName, model.LastName);

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };

            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            model.ReturnUrl = returnUrl;

            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                if (user == null)
                {
                    ModelState.AddModelError("", "رمزعبور یا شماره موبایل اشتباه است");
                    return View(model);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیقه قفل شده است";
                    return View(model);
                }

                ModelState.AddModelError("", "رمزعبور یا شماره موبایل اشتباه است");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> IsUserNameInUse(string userName)
        //{
        //    var user = await _userManager.FindByNameAsync(userName);
        //    if (user == null) return Json(true);
        //    return Json("نام کاربری وارد شده از قبل موجود است");
        //}

        private async Task<T> CallApiSMSPanel<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {

                    return default(T);
                }
                return default(T);
            }
        }

    }
}
