using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.ViewModels
{
    //[Table("WebInfo")]
    public class WebInfoViewModels
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Hình ảnh")]
        public string ImageLogo { get; set; }
        public string TitleLogo { get; set; }
        public string  Slogone { get; set; }
        public string  CompanyTitle{ get; set; }
        public string  ShortDescription{ get; set; }
        public string  Description{ get; set; }
        public string  Address{ get; set; }
        public string PhoneNumber{ get; set; }
        public string Hotline{ get; set; }
        public string Hotline2{ get; set; }
        public string Email{ get; set; }
        public string  Website{ get; set; }
        public string  TitleWeb{ get; set; }
        public string  MainKeywordH1{ get; set; }
        public string Seokeyword{ get; set; }
        public string  Seodescription{ get; set; }
        public string  SeoGooglesiteverification{ get; set; }
        public string  SeoAuthor{ get; set; }
        public string  SeoRevisitafter{ get; set; }
        public string  Seorobots{ get; set; }
        public string  Seogeoregion{ get; set; }
        public string  Seogeoplacename{ get; set; }
        public string  Seogeoposition{ get; set; }
        public string  SeoICBM{ get; set; }
        public string  Seomsvalidate01{ get; set; }
        public string  Seocontentlanguage{ get; set; }
        public string  SeoCOPYRIGHT{ get; set; }
        public string  Seogoogleanalytics{ get; set; }
        public string  Fax{ get; set; }
        public string  Facebook{ get; set; }
        public string  Twitter{ get; set; }
        public string  Googleplus{ get; set; }
        public string  Telegram{ get; set; }
        public string  Skype{ get; set; }
        public string BaiViet1 { get; set; }
        public string BaiViet2 { get; set; }
        public int So1 { get; set; }
        public int So2 { get; set; }
    }
}
