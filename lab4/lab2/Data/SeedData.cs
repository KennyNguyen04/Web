using Microsoft.EntityFrameworkCore;
using lab2.Data; // Namespace chứa lab2Context của bạn

namespace lab2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new lab2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<lab2Context>>()))
            {
                // Kiểm tra xem đã có phim nào chưa
                if (context.Movie.Any())
                {
                    return;   // DB đã có dữ liệu
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = "R", // Thêm dòng này
                        Price = 7.99M
                    },
                    // ... Thêm Rating cho các phim khác tương tự
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }

                );
                context.SaveChanges();
            }
        }
    }
}