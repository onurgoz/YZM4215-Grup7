using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using YZM4215_Grup7.ApiServices.Interfaces;

namespace YZM4215_Grup7.TagHelpers
{
    [HtmlTargetElement("getproductimage")]
    public class ProductImageTagHelper : TagHelper
    {
        private readonly IImageService _imageService;
    
        public ProductImageTagHelper(IImageService imageService)
        {
            _imageService=imageService;
        }

        public int Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageService.GetProductImageById(Id);
            string html= string.Empty;

            html =$"<img class='card-img-top' src='{blob}' alt=''>";
        
            output.Content.SetHtmlContent(html);
        }
    }
}