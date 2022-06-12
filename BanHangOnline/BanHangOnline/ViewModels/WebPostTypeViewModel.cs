using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostTypeViewModel
    {
        [Key]
        public int PostTypeID { get; set; }
        public string PostTypeTitle { get; set; }
        public string PostTypeReadmore { get; set; }
        public string PostTypeImage { get; set; }
        public bool PostTypeEnable { get; set; }
    }
}
