using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.CustomFilters;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize(Roles="Admin")]
    public class HomeController : Controller{
        public IActionResult Index(){
            return View();
        }
    }
    
}