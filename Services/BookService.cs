using LibraryDemo.DTOs;
using LibraryDemo.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryDemo.Services
{
    public class BookService : IBookService
    {
        HttpClient client;
        JsonSerializerOptions options;
        Uri uri = new Uri("https://localhost:7196/api/Book");
        public BookService() { 
            client = new HttpClient();
            options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddBook(Book book)
        {
            if(book.Id == 0)
            {
                var json = JsonSerializer.Serialize(book);
                var content = new StringContent(json, Encoding.UTF8, "application/type");
                await client.PostAsync(uri, content);
            }
            
        }

        public async Task DeleteBook(int id)
        {
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            await client.DeleteAsync($"{uri}/{id}");
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<List<BookDTO>>(content, options);
            
            return json;
        }

        public async Task<Book> GetBookById(int id)
        {
            var response = await client.GetAsync($"{uri}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<Book>(content, options);

            return json;
        }

        public async Task UpdateBook(Book book)
        {
            if (book.Id != 0) { 
                var json = JsonSerializer.Serialize(book);
                var content = new StringContent(json, Encoding.UTF8, "application/type");

                await client.PutAsync(uri, content);
            }
        }
    }
}
