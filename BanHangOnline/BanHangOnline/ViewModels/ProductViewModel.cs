using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string ShortDescription{ get; set; }
        public string Description{ get; set; }
        public string ProductSeoKeywords{ get; set; }
        public int ManufacturerId { get; set; }
        public int UnitTypeId { get; set; }
        public int TaxRateId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Point { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool ShowOnSalePage { get; set; }
        public int WarehouseId { get; set; }
        public int ProductTypeId { get; set; }
        public bool IsGroup { get; set; }
        public bool OwenSale { get; set; }
        public int QuantityInStock { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string ProductTitleURL{ get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy{ get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy{ get; set; }
    }
}
