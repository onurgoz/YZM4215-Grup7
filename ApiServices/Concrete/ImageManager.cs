using System;
using System.Net.Http;
using System.Threading.Tasks;
using YZM4215_Grup7.ApiServices.Interfaces;

namespace YZM4215_Grup7.ApiServices.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly HttpClient _httpClient;
        public ImageManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new System.Uri("http://localhost:58546/api/files/");
        }
        public async Task<string> GetProductImageById(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetImageByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }
            else
            {
                return null;
            }

        }
    }
}