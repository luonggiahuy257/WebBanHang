using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        [DisplayName("Id dành riêng cho caterory")]
        public int Id { get; set; }
        [DisplayName("Id dành riêng cho caterory")]
        public int? ParentId { get; set; }
        [DisplayName("Tên của danh mục")]
        public string CategoryName { get; set; }
        [DisplayName("Mô tả ngắn của danh mục")]
        public string ShortDescription { get; set; }
        [DisplayName("Mô tả danh mục")]
        public string Description{ get; set; }
        [DisplayName("Hình ảnh của danh mục")]
        public string CategoryImages{ get; set; }
        [DisplayName("Đường đẫn URL")]
        public string CategoryURL{ get; set; }
        [DisplayName("KeywordSeo")]
        public string CategorySeoKeywords { get; set; }
        [DisplayName("Hiển thị ở trang chủ")]
        public bool CategoryShowHome { get; set; }
        [DisplayName("Trạng thái danh mục")]
        public bool CategoryEnable { get; set; }
        [DisplayName("Thứ tự sắp xếp")]
        public int? CategoryOrder { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedAt { get; set; }
        [DisplayName("Người tạo")]
        public string CreatedBy{ get; set; }
        [DisplayName("Ngày cập nhập")]
        public DateTime? UpdatedAt { get; set; }
        [DisplayName("Người cập nhập")]
        public string UpdatedBy { get; set; }
    }
}
