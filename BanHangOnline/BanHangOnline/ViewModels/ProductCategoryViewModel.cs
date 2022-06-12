using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProductCategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int ProductCategoryOrder { get; set; }
        public bool ProductCategoryEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
