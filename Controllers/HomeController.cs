using Microsoft.AspNetCore.Mvc;

namespace YZM4215_Grup7.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}