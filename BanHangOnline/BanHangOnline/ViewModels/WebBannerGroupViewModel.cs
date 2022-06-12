using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebBannerGroupViewModel
    {
        [Key]
        public int BannerGroupID { get; set; }
        public string BannerGroupName { get; set; }
        public bool BannerGroupEnable { get; set; }
    }
}
