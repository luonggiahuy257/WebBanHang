using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.ViewModels
{
    public class RegisterViewModel
    {
        
        [DisplayName("Họ Tên Đệm")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên bắt buộc nhập")]
        [DisplayName("Tên đăng nhập (*)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email bắt buộc nhập")]
        [DisplayName("Email (*)")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password bắt buộc nhập")]
        [DisplayName("Mật khẩu (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Nhập lại mật khẩu (*)")]
        [Compare("Password", ErrorMessage = "Nhập Password không giống nhau")]
        public string ConfirmPassword { get; set; }


        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Giới tính")]
        public int GenderId { get; set; }
    }
}
