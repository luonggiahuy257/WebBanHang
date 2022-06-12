using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CityViewModel
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int StateProvinceId { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Abbreviation{ get; set; }
        public string GPSAddres { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
