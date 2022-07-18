using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.ViewModels
{
    [Table("WebBannerGroup")]
    public class WebBannerGroupViewModel
    {
        [Key]
        public int BannerGroupID { get; set; }
        public string BannerGroupName { get; set; }
        public bool BannerGroupEnable { get; set; }
    }
}
