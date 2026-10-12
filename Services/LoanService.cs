using Microsoft.EntityFrameworkCore;
using MyLibraryApp.Data;
using MyLibraryApp.Models;

namespace MyLibraryApp.Services;

public class LoanService : ILoanService
{
    private readonly LibraryContext _context;

    public LoanService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, Loan? Loan)> BorrowBookAsync(int memberId, int bookId)
    {
        var member = await _context.Members.FindAsync(memberId);
        if (member == null)
        {
            return (false, "Member not found", null);
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            return (false, "Book not found", null);
        }

        var activeLoans = await _context.Loans
            .CountAsync(l => l.MemberId == memberId && l.ReturnDate == null);

        if (activeLoans >= member.MaxBooksAllowed)
        {
            return (false, "Member has reached the maximum number of borrowed books", null);
        }

        if (!book.BorrowCopy())
        {
            return (false, "No copies available", null);
        }

        var loan = new Loan
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return (true, "Book borrowed successfully", loan);
    }

    public async Task<(bool Success, string Message)> ReturnBookAsync(int loanId)
    {
        var loan = await _context.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId);
        if (loan == null)
        {
            return (false, "Loan not found");
        }

        if (loan.ReturnDate != null)
        {
            return (false, "Book already returned");
        }

        loan.ReturnDate = DateTime.Now;
        loan.Book?.ReturnCopy();

        await _context.SaveChangesAsync();

        return (true, "Book returned successfully");
    }

    public async Task<List<Loan>> GetLoansByMemberAsync(int memberId)
    {
        return await _context.Loans
            .Where(l => l.MemberId == memberId)
            .Include(l => l.Book)
            .ToListAsync();
    }

    public async Task<List<Loan>> GetOverdueLoansAsync()
    {
        return await _context.Loans
            .Where(l => l.ReturnDate == null && l.DueDate < DateTime.Now)
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync();
    }
}
