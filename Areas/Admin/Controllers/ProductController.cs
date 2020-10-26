using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    
    public class ProductController : Controller{

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }


        [HttpGet]
        [JwtAuthorize(Roles="Admin,Member")]
        public async Task<IActionResult> GetAllProducts(){
            return View(await _productService.GetAllProductsAsync());
        }

        
        [JwtAuthorize(Roles="Admin")]
        public IActionResult AddProduct(){
            return View(new ProductAddModel());
        }

        [HttpPost]
        [JwtAuthorize(Roles="Admin")]
        public async Task<IActionResult> AddProduct(ProductAddModel model){
            await _productService.AddProductAsync(model);
            return RedirectToAction("Index","Home");
        }

    }
}