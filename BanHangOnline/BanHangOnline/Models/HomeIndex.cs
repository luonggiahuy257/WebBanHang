using BanHangOnline.ViewModels;
using System.Collections.Generic;

namespace BanHangOnline.Models
{
    public class HomeIndex
    {
        public List<WebBannerViewModel> WebBanner { get; set; }
        public ProductViewModel Product { get; set; }

        public ProductImageViewModel ProductImage { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
