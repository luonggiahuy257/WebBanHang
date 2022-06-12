using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class WebPostTagViewModel
    {
        [Key]
        public int PostTagID { get; set; }
        public string PostTagTitle { get; set; }
        public string PostTagLowerTitle { get; set; }
        public string PostTagReadmore { get; set; }
        public string PostTagContent { get; set; }
        public string PostTagImage{ get; set; }
        public string PostTagTitleImage{ get; set; }
        public string PostTagtTitleAlt{ get; set; }
        public string PostTagSeoKeyword{ get; set; }
        public string PostTagTitleURL{ get; set; }
        public string PostTagTitleGoogle { get; set; }
        public bool PostTagEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
