using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.ViewModels
{
    [Table("Cart")]
    public class CartViewModelCheckOut
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string Token{ get; set; }
        public bool Status { get; set; }
        [DisplayName("Trạng thái đơn hàng")]
        public int OrderStatusId { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        public string Contents{ get; set; }
        public string FirstName{ get; set; }
        [DisplayName("Tên người đặt")]
        public string LastName{ get; set; }
        [DisplayName("Số Điện thoại")]
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        public string AddressExt { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public int? GenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
