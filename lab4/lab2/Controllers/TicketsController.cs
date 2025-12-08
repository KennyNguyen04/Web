using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab2.Data;
using lab2.Models;

namespace lab2.Controllers
{
    public class TicketsController : Controller
    {
        private readonly lab2Context _context;

        public TicketsController(lab2Context context)
        {
            _context = context;
        }

        // GET: Tickets
        // GET: Tickets
        public async Task<IActionResult> Index(string searchString)
        {
            // Bắt đầu truy vấn và Include luôn thông tin Phim và Khách hàng để hiển thị
            var tickets = _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .AsQueryable(); // Chuyển về IQueryable để có thể nối thêm điều kiện Where

            if (!String.IsNullOrEmpty(searchString))
            {
                // Tìm theo tên Phim HOẶC tên Khách hàng
                tickets = tickets.Where(t => t.Movie!.Title!.Contains(searchString)
                                          || t.Customer!.FullName!.Contains(searchString));
            }

            return View(await tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title"); // Sửa thành Title
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // 1. Xóa "Price" khỏi danh sách Bind để tránh hacker inject giá tiền
        public async Task<IActionResult> Create([Bind("Id,SeatNumber,PurchaseDate,MovieId,CustomerId")] Ticket ticket)
        {

            // 2. Logic lấy giá vé từ phim
            // Tìm bộ phim đang được chọn mua vé
            var movie = await _context.Movie.FindAsync(ticket.MovieId);

            if (movie != null)
            {
                // Gán giá của phim sang giá của vé
                ticket.Price = movie.Price;
            }
            else
            {
                // Xử lý trường hợp không tìm thấy phim (dù hiếm khi xảy ra nếu chọn từ dropdown)
                ModelState.AddModelError("", "Movie not found!");
            }

            // 3. Kiểm tra tính hợp lệ
            // Bỏ qua lỗi validation của trường Price (vì ta vừa gán code-behind, ModelState có thể chưa biết)
            ModelState.Remove("Price");

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi, load lại Dropdown để hiển thị lại form
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeatNumber,Price,PurchaseDate,MovieId,CustomerId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
