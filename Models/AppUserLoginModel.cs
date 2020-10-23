using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class AppUserLoginModel
    {
        [Required(ErrorMessage="Email alanı boş geçilemez")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="Şifre alanı boş geçilemez")]
        public string Password { get; set; }
    }
}