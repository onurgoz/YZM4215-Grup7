using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.CustomFilters;

namespace YZM4215_Grup7.Areas.Member.Controllers
{
    [Area("Member")]
    [JwtAuthorize(Roles="Member")]
    public class HomeController : Controller
    {
        public IActionResult Index(){
            TempData["Active"] = "home";
            return View();
        }
    }    
}