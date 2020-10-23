using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class AppUserRegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı boş geçilemez")]
        [StringLength(18,ErrorMessage = "Kullanıcı adı maksimum {1}, minimum  {2} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [StringLength(18,ErrorMessage = "Şifre maksimum {1}, minimum  {2} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Yeniden şifre alanı boş geçilemez")]
        public string CheckPassword { get; set; }
        
        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Tam ad alanı boş geçilemez")]
        [StringLength(18,ErrorMessage = "Tam Ad maksimum {1}, minimum  {2} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
        public string FullName { get; set; }

    }
}