using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.ViewModels
{
    [Table("Product")]
    public class ProductViewModel
    {
        [Key]
        [DisplayName("Id của sản phẩm")]
        public int Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        
        [DisplayName("Id của danh mục cha")]
        public int CategoryId { get; set; }

        [DisplayName("Mã sku")]
        public string SKU { get; set; }

        [DisplayName("Mô tả ngắn")]
        public string ShortDescription{ get; set; }

        [DisplayName("Mô tả chi tiết")]
        public string Description{ get; set; }

        [DisplayName("KeywordSeo")]
        public string ProductSeoKeywords{ get; set; }

        [DisplayName("Nhà sản xuất")]
        public int ManufacturerId { get; set; }

        [DisplayName("Loại đơn vị")]
        public int UnitTypeId { get; set; }

        [DisplayName("Thuế suất")]
        public int TaxRateId { get; set; }

        [DisplayName("Giá bán")]
        public decimal SalePrice { get; set; }

        [DisplayName("Giá bán lẽ")]
        public decimal RetailPrice { get; set; }

        [DisplayName("Điểm")]
        public decimal Point { get; set; }
        [DisplayName("Sản phẩm yêu thích")]
        public bool IsWish { get; set; }

        [DisplayName("Hiển thị ở trang chủ")]
        public bool ShowOnHomePage { get; set; }

        [DisplayName("Hiển thị ở trang sale")]
        public bool ShowOnSalePage { get; set; }

        [DisplayName("Id kho hàng")]
        public int WarehouseId { get; set; }

        [DisplayName("Loại sản phẩm")]
        public int ProductTypeId { get; set; }

        [DisplayName("Thuộc nhóm sản phẩm đặt biệt")]
        public bool IsGroup { get; set; }

        [DisplayName("Giảm giá")]
        public bool OwenSale { get; set; }

        [DisplayName("Số lượng sản phẩm trong kho")]
        public int QuantityInStock { get; set; }

        [DisplayName("Thứ tự hiển thị")]
        public int DisplayOrder { get; set; }

        [DisplayName("Sản phẩm được bán tiếp tục")]
        public bool Published { get; set; }

        [DisplayName("Trạng thái delete sản phẩm")]
        public bool Deleted { get; set; }

        [DisplayName("Đường đẫn url sản phẩm")]
        public string ProductTitleURL{ get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }

        [DisplayName("Ngày cập nhập")]
        public DateTime UpdatedAt { get; set; }

        [DisplayName("Người cập nhập")]
        public string UpdatedBy { get; set; }
    }
}
