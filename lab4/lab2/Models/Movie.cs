using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tên phim phải từ 3 đến 60 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên phim")]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Thể loại phải bắt đầu bằng chữ hoa và chỉ chứa chữ cái")]
        [Required(ErrorMessage = "Vui lòng nhập thể loại")]
        [StringLength(30)]
        public string? Genre { get; set; }

        [Range(1, 100, ErrorMessage = "Giá phải từ 1 đến 100")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Rating phải bắt đầu bằng chữ hoa")]
        [StringLength(5)]
        [Required(ErrorMessage = "Vui lòng nhập Rating")]
        public string? Rating { get; set; }
    }
}