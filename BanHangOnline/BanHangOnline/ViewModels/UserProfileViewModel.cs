using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class UserProfileViewModel
    {
        [Key]
        public string NetId { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Images { get; set; }
        public string DescriptionUser { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string AddressInfo { get; set; }
        public string AddressExt { get; set; }
        public string ZipPostalCode { get; set; }
        public string PasswordHash { get; set; }
        public string TaxCode { get; set; }
        public int? Admin { get; set; }
        public int? Vendor { get; set; }
        public bool? Locked { get; set; }
        public bool? Hidden { get; set; }
        public DateTime? LastDateLogin { get; set; }
        public DateTime? Birthday { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public int? GenderId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
    }
}
