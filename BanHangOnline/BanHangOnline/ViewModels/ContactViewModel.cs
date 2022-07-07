using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ContactViewModel
    {
        [Key]
        [DisplayName("Id liên hệ")]
        public int Id { get; set; }
        [DisplayName("Tên liên hệ")]
        public string Name { get; set; }
        [DisplayName("Tên đầy đủ")]
        public string FullName { get; set; }
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName("Thư điện tử")]
        public string Email { get; set; }
        [DisplayName("Thông tin chi tiết")]
        public string Contents { get; set; }
        [DisplayName("Thông tin ngắn")]
        public string ShortContent { get; set; }
        [DisplayName("Hình ảnh liên hệ")]
        public string ContactImages { get; set; }
        [DisplayName("Đường dẫn URL")]
        public string ContactURL { get; set; }
        [DisplayName("Trang thái chờ")]
        public bool IsGroup { get; set; }
        [DisplayName("Trạng thái hoạt động")]
        public bool ContactEnable { get; set; }
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
