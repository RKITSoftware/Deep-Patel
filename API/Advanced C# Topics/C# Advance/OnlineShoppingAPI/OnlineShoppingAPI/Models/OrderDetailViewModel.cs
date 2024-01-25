using System;

namespace OnlineShoppingAPI.Models
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Guid InvoiceId { get; set; }
    }
}