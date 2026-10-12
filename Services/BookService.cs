using Microsoft.EntityFrameworkCore;
using MyLibraryApp.Data;
using MyLibraryApp.Models;

namespace MyLibraryApp.Services;

public class BookService : IBookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> UpdateAsync(int id, Book book)
    {
        var existing = await _context.Books.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.ISBN = book.ISBN;
        existing.TotalCopies = book.TotalCopies;
        existing.AvailableCopies = book.AvailableCopies;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Books.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _context.Books.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
