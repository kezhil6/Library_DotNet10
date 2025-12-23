using Library.Books.Core.Entities;
using Library.Books.Core.Interfaces.Repositories;
using Library.Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _repo;
        public BookRepository(BooksContext repo)
        {
                _repo = repo;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            _repo.Books.Add(book);
            await _repo.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var existingBook = await _repo.Books.FindAsync(id);
            if (existingBook == null)
                return false;

            _repo.Books.Remove(existingBook);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? author, string? genre)
        {
            var query = _repo.Books.AsQueryable();

            if(!string.IsNullOrEmpty(author))
                query = query.Where(q => q.Author == author);
            if(!string.IsNullOrEmpty(genre))
                query = query.Where(q => q.Genre == genre);

            return await query.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _repo.Books.FindAsync(id);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var existingBook = await _repo.Books.FindAsync(book.Id);
            if (existingBook == null)
                return null;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.PublishedYear = book.PublishedYear;

            await _repo.SaveChangesAsync();
            return existingBook;
        }
    }
}
