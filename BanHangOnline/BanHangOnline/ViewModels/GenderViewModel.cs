using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class GenderViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
