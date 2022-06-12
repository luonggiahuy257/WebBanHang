using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class OfficeTypeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
