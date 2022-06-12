using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostBannerGroupViewModel
    {
        [Key]
        public int BannerGroupID { get; set; }
        public string BannerGroupName { get; set; }
        public bool BannerGroupEnable { get; set; }
    }
}
