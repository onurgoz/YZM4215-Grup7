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
            // var user = _contextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            // if (user != null)
            // {/*burası değieşecektir.<------------------------------------------*/
            //     return RedirectToAction("Index", "Home");
            // }
            // else
            // {
            if (ModelState.IsValid)
            {
                if (await _authService.SignIn(model))
                {
                    return RedirectToAction("Index", "Home", new { @area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View(model);
            //}

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
                string message= await _authService.SignUp(model);
                if (string.IsNullOrWhiteSpace(message))
                {
                    TempData["Kayit"] = "KayıtOlundu!";
                    return RedirectToAction("LogIn", "Auth");
                }
                else{
                    ModelState.AddModelError("",message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}