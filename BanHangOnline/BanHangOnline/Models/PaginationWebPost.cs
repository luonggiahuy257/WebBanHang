using BanHangOnline.ViewModels;
using System.Collections.Generic;

namespace BanHangOnline.Models
{
    public class PaginationWebPost
    {
        public List<WebPostViewModel> WebPosts { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
