using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Extensions;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IDealerService _dealerService;
        private readonly IHttpContextAccessor _accessor;
        public OrderController(IHttpContextAccessor accessor, IDealerService dealerService, IProductService productService, IOrderService orderService)
        {
            _orderService = orderService;
            _productService = productService;
            _dealerService = dealerService;
            _accessor = accessor;
        }


        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetAllOrders());
        }


        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Buy(int productId)
        {
            var product = await _productService.GetByIdProductAsync(productId);
            var user = _accessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            var dealer = await _dealerService.GetDealersByAppUserId(user.Id);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }
            else if (dealer == null)
            {
                return RedirectToAction("Index", "Dealer");
            }

            else
            {
                ViewBag.Dealers = new SelectList(dealer, "Id", "Name");
                ViewBag.Product = product.Name;
                return View(new AddOrderModel
                {
                    ProductId = productId
                });
            }
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Buy(AddOrderModel model)
        {
            if (ModelState.IsValid)
            {
                string message = await _orderService.AddOrder(model);
                if (string.IsNullOrWhiteSpace(message))
                {
                    TempData["Offer"] = "success";
                    return RedirectToAction("Index","Order");
                }
                else{
                    ModelState.AddModelError("","Sistemde bir hata oluştu. En kısa zamanda düzeltilecek.");
                    return View(model);
                }
            }
            else
            {
                var user = _accessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
                ViewBag.Dealers= new SelectList(await _dealerService.GetDealersByAppUserId(user.Id),"Id","Name");
                return View(model);
            }
        }

    }
}