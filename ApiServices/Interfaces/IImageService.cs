using System.Threading.Tasks;

namespace YZM4215_Grup7.ApiServices.Interfaces
{
    public interface IImageService
    {
        Task<string> GetProductImageById(int id);
        Task<string> GetProfileImageById(int id);
    }
}