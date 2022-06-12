using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class TaxRateViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ValueTax { get; set; }
        public bool IsPercent { get; set; }
        public string Discription { get; set; }
    }
}
