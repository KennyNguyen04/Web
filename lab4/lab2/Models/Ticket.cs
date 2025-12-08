using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số ghế")]
        [StringLength(10, ErrorMessage = "Số ghế tối đa 10 ký tự")]
        [Display(Name = "Seat Number")]
        public string? SeatNumber { get; set; }

        // --- ĐÃ SỬA ĐOẠN NÀY ---
        // Bỏ [Range] vì giá vé sẽ được code tự động gán từ phim
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Ticket Price")]
        public decimal Price { get; set; }
        // ------------------------

        [DataType(DataType.DateTime)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now; // Mặc định là thời gian hiện tại

        // --- KHÓA NGOẠI (Foreign Keys) ---

        // Liên kết tới Movie (Phim)
        [Display(Name = "Movie")]
        public int MovieId { get; set; } // Khóa ngoại
        public Movie? Movie { get; set; } // Navigation property

        // Liên kết tới Customer (Khách hàng)
        [Display(Name = "Customer")]
        public int CustomerId { get; set; } // Khóa ngoại
        public Customer? Customer { get; set; } // Navigation property
    }
}