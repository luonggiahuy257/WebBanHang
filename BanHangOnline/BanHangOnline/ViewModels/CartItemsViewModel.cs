using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CartItemsViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string SKU { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string Contents { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
