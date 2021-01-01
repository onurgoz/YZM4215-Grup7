using System.Collections.Generic;
using System.Threading.Tasks;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel> GetByIdProductAsync(int id);
        Task<bool> AddProductAsync(ProductAddModel model);
        Task<string> UpdateProductAsync(ProductUpdateModel model);
        Task<string> DeleteProductAsync(int id);
    }
}