using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostInTagViewModel
    {
        [Key]
        public int PostInTagID { get; set; }
        public int PostID { get; set; }
        public int PostTagID { get; set; }
    }
}
