using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class OrderItemsViewModel
    {
        [Key]
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Point { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal  TotalPrice { get; set; }
    }
}
