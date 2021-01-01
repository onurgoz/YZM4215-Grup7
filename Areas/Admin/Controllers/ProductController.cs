using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.CustomFilters;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize(Roles = "Admin")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            return View(await _productService.GetByIdProductAsync(id));
        }

        [JwtAuthorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View(new ProductAddModel());
        }

        [HttpPost]
        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductAddModel model)
        {

            if (await _productService.AddProductAsync(model))
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Resim yükleme hatası! Lütfen JPG veya PNG türünde yükleme yapınız. Resmin boyutu 1MB'ı geçemez");
                return View(model);
            }

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
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Resim yükleme hatası! Lütfen JPG veya PNG türünde yükleme yapınız");
                    return View(model);
                }
            }
            return View(model);
        }

        [JwtAuthorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            string message = await _productService.DeleteProductAsync(id);
            if (string.IsNullOrWhiteSpace(message))
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Api hatası. Lütfen yetkiliye bildirin");
                return RedirectToAction("Index", "Product");
            }
        }
    }
}