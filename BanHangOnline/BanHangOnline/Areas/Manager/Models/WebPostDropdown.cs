using BanHangOnline.ViewModels;
using System;
using System.Collections.Generic;

namespace BanHangOnline.Areas.Manager.Models
{
    public class WebPostDropdown
    {
        public int PostID { get; set; }
        public int? PostParentID { get; set; }
        public int? PostTypeID { get; set; }
        public string PostTitle { get; set; }
        public string PostLowerTitle { get; set; }
        public string PostReadmore { get; set; }
        public string PostContent { get; set; }
        public string PostUserName { get; set; }
        public string PostImage { get; set; }
        public bool PostEnable { get; set; }
        public string PostSeoKeyword { get; set; }
        public string PostTitleURL { get; set; }
        public string PostTitleGoogle { get; set; }
        public string PostTitleImage { get; set; }
        public string PostTitleAlt { get; set; }
        public DateTime PostLastModified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<WebPostParentViewModel> WebPostParent { get; set; }
    }
}
