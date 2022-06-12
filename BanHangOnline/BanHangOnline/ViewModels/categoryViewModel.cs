using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class categoryViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string Description{ get; set; }
        public string CategoryImages{ get; set; }
        public string CategoryURL{ get; set; }
        public string CategorySeoKeywords { get; set; }
        public bool CategoryShowHome { get; set; }
        public bool CategoryEnable { get; set; }
        public int CategoryOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy{ get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
