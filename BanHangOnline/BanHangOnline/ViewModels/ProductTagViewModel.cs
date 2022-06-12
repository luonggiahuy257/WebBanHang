using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProductTagViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
    }
}
