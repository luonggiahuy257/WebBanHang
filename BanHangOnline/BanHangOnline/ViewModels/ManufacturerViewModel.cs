using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ManufacturerViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Logo{ get; set; }
        public string Picture{ get; set; }
        public string Email{ get; set; }
        public int TownId { get; set; }
        public string Address{ get; set; }
        public string AddressExt{ get; set; }
        public string ZipPostalCode{ get; set; }
        public string PhoneNumber{ get; set; }
        public string MobileNumber{ get; set; }
        public string FaxNumber{ get; set; }
        public string BankAccount{ get; set; }
        public string BankName{ get; set; }
        public string TaxCode{ get; set; }
        public string About{ get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CoperativeDate { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
    }
}
