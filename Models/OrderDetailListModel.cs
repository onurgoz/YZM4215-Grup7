using System;

namespace YZM4215_Grup7.Models
{
    public class OrderDetailListModel
    {
        public int Id { get; set; }
        public int NumberOfOrders { get; set; }
        public DateTime BuyTime { get; set; }

        public string ProductName { get; set; }
        public string DealerName { get; set; }
    }
}