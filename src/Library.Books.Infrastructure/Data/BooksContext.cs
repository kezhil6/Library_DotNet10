using Library.Books.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Books.Infrastructure.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
