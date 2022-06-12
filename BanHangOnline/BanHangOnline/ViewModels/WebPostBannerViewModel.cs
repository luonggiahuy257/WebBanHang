using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostBannerViewModel
    {
        [Key]
        public int BannerID { get; set; }
        public string BannerImage { get; set; }
        public string BannerTitle { get; set; }
        public string BannerAlt{ get; set; }
        public string BannerUrl { get; set; }
        public bool BannerEnable { get; set; }
        public int BannerGroupID { get; set; }
        public string BannerDescription { get; set; }
    }
}
