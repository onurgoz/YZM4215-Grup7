using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize(Roles="Admin")]
    public class ProductController : Controller
    {
        
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }

        public async Task<IActionResult> ProductDetail(int id){
            return View(await _productService.GetByIdProductAsync(id));
        }

        public IActionResult AddProduct()
        {
            return View(new ProductAddModel());
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductAddModel model)
        {
            await _productService.AddProductAsync(model);
            return RedirectToAction("Index", "Product");
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productService.GetByIdProductAsync(id);

            return View(new ProductUpdateModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
            });
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                string message = await _productService.UpdateProductAsync(model);
                if (string.IsNullOrWhiteSpace(message))
                {
                    return RedirectToAction("Index","Product");
                }
                else{
                    ModelState.AddModelError("","Api hatasý! Lütfen düzeltiniz");
                    return View(model);
                }
            }
            return View(model);
        }

        [JwtAuthorize(Roles="Admin")]
        public async Task<IActionResult> DeleteProduct(int id){
            string message = await _productService.DeleteProductAsync(id);
             if (string.IsNullOrWhiteSpace(message))
                {
                    return RedirectToAction("Index","Product");
                }
                else{
                    ModelState.AddModelError("","Api hatasý! Lütfen düzeltiniz");
                    return RedirectToAction("Index","Product");
                }
        }
    }
}