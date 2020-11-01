using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using YZM4215_Grup7.ApiServices.Interfaces;
using YZM4215_Grup7.Models;

namespace YZM4215_Grup7.ApiServices.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;
        public ProductManager(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _accessor=accessor;
            _httpClient=httpClient;
            _httpClient.BaseAddress=new System.Uri("http://localhost:58546/api/products/");
        }
        
        public async Task<List<ProductViewModel>> GetAllProductsAsync(){
            var responseMessage = await _httpClient.GetAsync("");

            if(responseMessage.IsSuccessStatusCode){
                return JsonConvert.DeserializeObject<List<ProductViewModel>> (await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<ProductViewModel> GetByIdProductAsync(int id){
            var responseMessage = await _httpClient.GetAsync($"{id}");

            if(responseMessage.IsSuccessStatusCode){
                return JsonConvert.DeserializeObject<ProductViewModel> (await responseMessage.Content.ReadAsStringAsync());
            }
            else{
                return null;
            }
        }

        public async Task AddProductAsync(ProductAddModel model){
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if(model.Image != null){
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();

                ByteArrayContent arrayContent = new ByteArrayContent(bytes);

                arrayContent.Headers.ContentType= new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(arrayContent,nameof(ProductAddModel.Image),model.Image.FileName);
            }

            formData.Add(new StringContent(model.Name),nameof(ProductAddModel.Name));
            formData.Add(new StringContent(model.Description),nameof(ProductAddModel.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("",formData);

        }


        public async Task<string> UpdateProductAsync(ProductUpdateModel model){
            MultipartFormDataContent dataContent = new MultipartFormDataContent();
            if(model.Image!=null){
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();

                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                dataContent.Add(byteContent,nameof(model.Image),model.Image.FileName); 
            }

            dataContent.Add(new StringContent(model.Id.ToString()),nameof(model.Id));
            dataContent.Add(new StringContent(model.Name),nameof(model.Name));
            dataContent.Add(new StringContent(model.Description),nameof(model.Description));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));

            var responseMessage =await _httpClient.PutAsync($"{model.Id}",dataContent);

            if(responseMessage.IsSuccessStatusCode){
                return "";
            }
            else{
                return "api hatasý kontrol et";
            }
        }


        public async Task<string> DeleteProductAsync(int id){
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("token"));
            var responseMessage = await _httpClient.DeleteAsync($"{id}");
            if(responseMessage.IsSuccessStatusCode){
                return "";
            }
            else{
                return "api hatasý kontrol et";
            }
        }
    }
}