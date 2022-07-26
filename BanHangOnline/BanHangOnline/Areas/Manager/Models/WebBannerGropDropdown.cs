using BanHangOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.Areas.Manager.Models
{
    public class WebBannerGropDropdown
    {
        [Key]
        public int BannerId { get; set; }
        [Required]
        [DisplayName("Hình ảnh")]
        public string BannerImage { get; set; }
        [DisplayName("Tên hình ảnh")]
        public string BannerTitle { get; set; }
        public string BannerDescription { get; set; }
        public string BannerAlt { get; set; }
        public string BannerURL { get; set; }
        public bool BannerEnable { get; set; }
        [DisplayName("Nhóm banner")]
        public int? BannerGroupId { get; set; }
        public int? PostParentId { get; set; }
        public List<WebBannerGroupViewModel> WebBannerGroup { get; set; }
    }
}
