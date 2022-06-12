using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CartViewModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string Token{ get; set; }
        public bool Status { get; set; }
        public string Contents{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AddressExt { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int GenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
