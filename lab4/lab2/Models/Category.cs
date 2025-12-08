using System.ComponentModel.DataAnnotations;

namespace lab2.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(50, ErrorMessage = "Tên thể loại không được vượt quá 50 ký tự")]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
        public string? Description { get; set; }
    }
}