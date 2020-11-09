using System.Collections.Generic;
using System.Threading.Tasks;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Interfaces
{
    public interface IDealerService
    {
        Task<List<DealerListModel>> GetAllDealersAsync();
        Task<DealerListModel> GetDealerByIdAsync(int id);
        Task<List<DealerListModel>> GetDealersByAppUserId(int appUserId);
        Task AddDealerAsync(DealerAddModel model);
        Task UpdateDealerAsync(DealerListModel model);
        Task DeleteDealerAsync(int id);

    }
}