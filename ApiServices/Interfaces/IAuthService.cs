using System.Threading.Tasks;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Interfaces
{
    public interface IAuthService{
        Task<bool> SignIn(AppUserLoginModel model);
        Task<string> SignUp(AppUserRegisterModel model);
    }
}