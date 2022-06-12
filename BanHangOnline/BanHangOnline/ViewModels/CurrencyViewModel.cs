using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CurrencyViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortCode{ get; set; }
        public string Description{ get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }

    }
}
