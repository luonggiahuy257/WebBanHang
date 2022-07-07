using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProductImageViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? DisplayOrder { get; set; }
        public bool Published { get; set; }
    }
}
