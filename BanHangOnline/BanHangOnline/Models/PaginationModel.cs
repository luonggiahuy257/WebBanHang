using BanHangOnline.ViewModels;
using System.Collections.Generic;

namespace BanHangOnline.Models
{
    public class PaginationModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
