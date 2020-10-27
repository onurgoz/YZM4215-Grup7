using Microsoft.AspNetCore.Http;

namespace YZM4215_Grup7.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public bool IsVisible { get; set; }
    }

}