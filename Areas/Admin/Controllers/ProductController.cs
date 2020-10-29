using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize(Roles="Admin")]
    public class ProductController : Controller{

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts(){
            return View(await _productService.GetAllProductsAsync());
        }

        
        public IActionResult AddProduct(){
            return View(new ProductAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddModel model){
            await _productService.AddProductAsync(model);
            return RedirectToAction("GetAllProducts","Product");
        }

    }
}