using System;
using System.ComponentModel.DataAnnotations;

namespace YZM4215_Grup7.Models
{
    public class AddOrderModel
    {
        [Range(1, 3000, ErrorMessage = "En az 1, en fazla 3000 adet sipariş verebilirsiniz")]
        [Required(ErrorMessage = "Sipariş alanı boş geçilemez")]
        public int NumberOfOrders { get; set; }

        public DateTime BuyTime { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Birşeyler ters gitti! Lütfen tekrar dene")]
        public int ProductId { get; set; }

        [Range(0, 50, ErrorMessage = "Lütfen bir bayi seçiniz")]
        public int DealerId { get; set; }
    }
}