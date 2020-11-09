using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpClient _httpClient;

        public OrderManager(IHttpContextAccessor accessor, HttpClient httpClient)
        {
            _accessor = accessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("http://localhost:58546/api/orders/");
        }


        public async Task<string> AddOrder(AddOrderModel model)
        {
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();

            formDataContent.Add(new StringContent(model.DealerId.ToString()), nameof(model.DealerId));
            formDataContent.Add(new StringContent(model.ProductId.ToString()), nameof(model.ProductId));
            formDataContent.Add(new StringContent(model.NumberOfOrders.ToString()), nameof(model.NumberOfOrders));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));

            var responseMessage = await _httpClient.PostAsync("", formDataContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return "";
            }
            else
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public async Task<List<OrderDetailListModel>> GetAllOrders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));

            var responseMessage = await _httpClient.GetAsync("");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<OrderDetailListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<List<OrderDetailListModel>> GetAllOrdersByAppUserIdAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));

            var responseMessage = await _httpClient.GetAsync($"{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<OrderDetailListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

    }
}