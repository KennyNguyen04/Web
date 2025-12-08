using System.ComponentModel.DataAnnotations;

namespace lab2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên phải từ 3 đến 100 ký tự")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}