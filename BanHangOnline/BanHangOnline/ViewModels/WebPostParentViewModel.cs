using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostParentViewModel
    {
        [Key]
        public int PostParentID { get; set; }
        public string PostParentTitle { get; set; }
        public string PostParentLowerTitle { get; set; }
        public string PostParentReadmore { get; set; }
        public string PostParentContent { get; set; }
        public string PostParentImage { get; set; }
        public bool PostParentEnable { get; set; }
        public string PostParentSeoKeyword{ get; set; }
        public string PostParentTitleURL{ get; set; }
        public string PostParentTitleGoogle{ get; set; }
        public string PostParentTitleImage{ get; set; }
        public string PostParentTitleAlt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
