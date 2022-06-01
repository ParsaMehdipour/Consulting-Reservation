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
                    TempData.Remove("PTCPhone");


                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "ثبت نام با موفقیت انجام شد"
                    };
                }

                TempData.Remove("PTCPhone");


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
        public async Task<ResultDto> ExpertConfirmSignup([FromForm] ExpertInformationForSignupConfirmationDto model)
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
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    UserName = totpTempDataModel.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {
                    var signedUser = await _userManager.FindByNameAsync(totpTempDataModel.PhoneNumber);

                    _registerAsExpertService.Execute(signedUser.Id, model.firstName, model.lastName);

                    TempData.Remove("PTCPhone");

                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "ثبت نام با موفقیت انجام شد"
                    };
                }

                TempData.Remove("PTCPhone");


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

                if (user.IsActive == false && user.UserFlag == DataAccess.Enums.UserFlag.Consumer)
                {
                    ModelState.AddModelError("", "حساب کاربری شما غیرفعال شده است ");
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

        [HttpGet]
        public IActionResult ResetPassword()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ResultDto> SendVerificationCodeForResetPassword([FromForm] PhoneNumberForVerificationCodeDto model)
        {
            User user = await _userManager.FindByNameAsync(model.phoneNumber);

            if (user == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "این شماره تماس در سیستم ثبت شده نیست"
                };
            }

            if (TempData.ContainsKey("PTCPhoneResetPass"))
            {
                TempData.Remove("PTCPhoneResetPass");
            }

            if (TempData.ContainsKey("PTCResetPass"))
            {
                var totpTempDataModel = JsonSerializer.Deserialize<PhoneTempDataModel>(TempData["PTCResetPass"].ToString()!);
                if (totpTempDataModel.ExpirationTime >= DateTime.Now)
                {
                    var differenceInSeconds = (int)(totpTempDataModel.ExpirationTime - DateTime.Now).TotalSeconds;
                    TempData.Keep("PTCResetPass");
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

            var code = _phoneTotpProvider.GenerateTotp(secretKey);

            var smsModel = new SMSModel()
            {
                toNum = model.phoneNumber,
                patternCode = SMSPatterns.ResetPasswordPatternCode,
                inputData = new List<Dictionary<string, string>>()
                {
                    new Dictionary<string, string>
                    {
                        {SMSInputs.Code, code },
                    },
                },
            };

            var uri = "https://ippanel.com/api/select";

            await CallApiSMSPanel<object>(uri, smsModel);

            TempData["PTCPhoneResetPass"] = JsonSerializer.Serialize(new PhoneNumberTempDataModel()
            {
                PhoneNumber = model.phoneNumber,
                ExpirationTime = DateTime.Now.AddMinutes(10)
            });

            TempData["PTCResetPass"] = JsonSerializer.Serialize(new PhoneTempDataModel()
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
        public ResultDto VerifyCodeForResetPassword(VerificationCodeForVerifyDto model)
        {
            if (!TempData.ContainsKey("PTCResetPass"))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا دوباره درخواست ارسال کد تایید دهید"
                };
            }

            var totpTempDataModel = JsonSerializer.Deserialize<PhoneTempDataModel>(TempData["PTCResetPass"].ToString()!);
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
                TempData.Remove("PTCResetPass");

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

        public async Task<ResultDto> ConfirmResetPassword([FromForm] PasswordForResetDto model)
        {
            if (TempData.ContainsKey("PTCPhoneResetPass"))
            {
                var totpTempDataModel = JsonSerializer.Deserialize<PhoneNumberTempDataModel>(TempData["PTCPhoneResetPass"].ToString()!);
                if (totpTempDataModel.ExpirationTime <= DateTime.Now)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا مجددا برای تغیر رمزعبور اقدام نمایید"
                    };
                }

                var user = await _userManager.FindByNameAsync(totpTempDataModel.PhoneNumber);

                await _userManager.RemovePasswordAsync(user);

                var result = await _userManager.AddPasswordAsync(user, model.password);

                if (result.Succeeded)
                {
                    TempData.Remove("PTCPhoneResetPass");


                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "تغیر رمزعبور با موفقیت انجام شد"
                    };
                }

                TempData.Remove("PTCPhoneResetPass");


                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تغیر رمزعبور با موفقیت انجام نشد لطفا مجددا تلاش نمایید"
                };

            }

            return new ResultDto()
            {
                IsSuccess = false,
                Message = "لطفا مجددا برای تغیر رمزعبور اقدام نمایید"
            };

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
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
