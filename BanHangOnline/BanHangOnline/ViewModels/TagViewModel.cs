using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class TagViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ParentTagId { get; set; }
        public string TitleTag { get; set; }
        public string TagSeoKeywords { get; set; }
        public string Description { get; set; }
        public string TagURL{ get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy{ get; set; }
    }
}
