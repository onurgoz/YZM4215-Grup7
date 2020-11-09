using System.Collections.Generic;
using System.Threading.Tasks;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Interfaces
{
    public interface IOrderService
    {
        Task<string> AddOrder(AddOrderModel model);
        Task<List<OrderDetailListModel>> GetAllOrders();
        Task<List<OrderDetailListModel>> GetAllOrdersByAppUserIdAsync(int id);
    }
}