using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.Models;
using YZM4215_Grup7.Extensions;

namespace YZM4215_Grup7.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;

        public AuthManager(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("http://localhost:58546/api/auth/");
            _accessor = accessor;
        }

        public async Task<bool> SignIn(AppUserLoginModel loginModel)
        {
            var json = JsonConvert.SerializeObject(loginModel);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        
            var responseMessage = await _httpClient.PostAsync("SignIn",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());
                _accessor.HttpContext.Session.SetString("token", token.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token.Token);
                var activeUser =await _httpClient.GetAsync("GetActiveUser");
            
                var userContent = JsonConvert.DeserializeObject<AppUserViewModel>(await activeUser.Content.ReadAsStringAsync());
                _accessor.HttpContext.Session.SetObject("activeUser", userContent);
                _accessor.HttpContext.Session.SetString("userName",userContent.FullName);
                return true;
            }
            return false;
        }


        public async Task<string> SignUp(AppUserRegisterModel model){
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();

            formDataContent.Add(new StringContent(model.UserName),nameof(AppUserRegisterModel.UserName));
            formDataContent.Add(new StringContent(model.Password),nameof(AppUserRegisterModel.Password));
            formDataContent.Add(new StringContent(model.Email),nameof(AppUserRegisterModel.Email));
            formDataContent.Add(new StringContent(model.FullName),nameof(AppUserRegisterModel.FullName));

            var responseMessage = await _httpClient.PostAsync("SignUp",formDataContent);

            if(!responseMessage.IsSuccessStatusCode){
                return "Sistemde bu kullanıcı adını veya email'ini kullanan birisi var. Lütfen başka bir kullanıcı adı veya email dene. ";
            }
            else{
                return "";
            }
            
        }
    }
}