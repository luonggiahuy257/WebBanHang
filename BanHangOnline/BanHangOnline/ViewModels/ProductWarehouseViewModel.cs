using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProductWarehouseViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OfficeId { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber{ get; set; }
        public string MobileNumber{ get; set; }
        public string Fax{ get; set; }
        public string About{ get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
    }
}
