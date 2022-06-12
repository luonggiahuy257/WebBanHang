using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class ProvinceViewModel
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Abbreviation { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
