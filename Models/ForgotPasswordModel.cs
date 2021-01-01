using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email alanı boş olamaz")]
        public string Email { get; set; }
    }
}