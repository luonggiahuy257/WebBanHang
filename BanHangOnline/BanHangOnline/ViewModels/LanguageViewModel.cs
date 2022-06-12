using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class LanguageViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoCode { get; set; }
        public string ThreeCode { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
