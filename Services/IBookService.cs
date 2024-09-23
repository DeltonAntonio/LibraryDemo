using LibraryDemo.DTOs;
using LibraryDemo.MVVM.Models;

namespace LibraryDemo.Services
{
    public interface IBookService
    {
        public Task<List<BookDTO>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task AddBook(Book book);
        public Task DeleteBook(int id);
        public Task UpdateBook(Book book);
    }
}
