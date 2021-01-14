using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Extensions;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IHttpContextAccessor contextAccessor, IAuthService authService)
        {
            _authService = authService;
            _contextAccessor = contextAccessor;
        }

        [LoginFilter]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(AppUserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                string message = await _authService.SignIn(model);
                if (message=="")
                {
                    var activeUser = _contextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
                    if (activeUser.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { @area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { @area = "Member" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", message);
                    return View(model);
                }
            }
            return View(model);
        }

        [LoginFilter]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string message = await _authService.SignUp(model);
                if (string.IsNullOrWhiteSpace(message))
                {
                    TempData["Kayit"] = "KayıtOlundu!";
                    return RedirectToAction("LogIn", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [LoginFilter]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (await _authService.ForgotMyPass(model))
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Böyle bir email bulunmamaktadır.");
                return View(model);
            }
        }

        [LoginFilter]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidModel]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (await _authService.ResetPassword(model))
            {
                return RedirectToAction("LogIn");
            }
            else
            {
                ModelState.AddModelError("", "Girdiğiniz kod yanlıştır.");
                return View(model);
            }
        }

        public IActionResult LogOut()
        {
            _contextAccessor.HttpContext.Session.Remove("token");
            _contextAccessor.HttpContext.Session.Remove("activeUser");
            return RedirectToAction("LogIn", "Auth");
        }

        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa Bulunamadı";
            }
            return View();
        }
    }
}