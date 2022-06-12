using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class CountryViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode{ get; set; }
        public int NumericIsoCode { get; set; }
        public int CurrencyId { get; set; }
        public bool AllowsBilling { get; set; }
        public bool AllowsShipping { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
