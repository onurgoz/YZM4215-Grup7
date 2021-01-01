using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Kod alanı gereklidir")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [StringLength(18, ErrorMessage = "Şifre maksimum {1}, minimum  {2} karakter uzunluğunda olmalıdır.", MinimumLength = 8)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Yeniden şifre alanı boş geçilemez")]
        public string CheckPassword { get; set; }
    }
}