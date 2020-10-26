using Microsoft.AspNetCore.Http;

namespace YZM4215_Grup7.Models
{
    public class ProductAddModel{
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}