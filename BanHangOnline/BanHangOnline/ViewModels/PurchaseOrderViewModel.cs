using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class PurchaseOrderViewModel
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string OrderCode { get; set; }
        public string SessionId{ get; set; }
        public string Token { get; set; }
        public int WarehouseId { get; set; }
        [Required]
        public string BuyerId{ get; set; }
        public string ApprovedById { get; set; }
        public int CountProduct { get; set; }
        public int CountProductType { get; set; }
        public decimal TotalPoint { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal FinalTotalPrice { get; set; }
        public int PaymentTypeId { get; set; }
        public bool AlreadyPaid { get; set; }
        public DateTime PaidDate { get; set; }
        public string Notes{ get; set; }
        public string ShipToCustomer{ get; set; }
        public string ShipToPhone{ get; set; }
        public DateTime ShipOnDate { get; set; }
        public int ShippingTime { get; set; }
        public string ShippingExpress { get; set; }
        public string ShipToAddress { get; set; }
        public int TownId { get; set; }
        public string ZipPostalCode { get; set; }
        public string ShippingCost { get; set; }
        public int OrderStatusId { get; set; }
        public int CurrencyId { get; set; }
        public string GuestEmail{ get; set; }
        public string GuestName{ get; set; }
        public string GuestPhone{ get; set; }
        public string GuestAddress { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy{ get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
    }
}
