using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class DealerListModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="İsim alanı boş geçilemez")]
        public string Name { get; set; }
        [Required(ErrorMessage="Adres alanı boş geçilemez")]
        public string Address { get; set; }
        public int AppUserId { get; set; }
    }
}