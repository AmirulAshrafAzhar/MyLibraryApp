using MyLibraryApp.Models;

namespace MyLibraryApp.Services;

public interface ILoanService
{
    Task<(bool Success, string Message, Loan? Loan)> BorrowBookAsync(int memberId, int bookId);
    Task<(bool Success, string Message)> ReturnBookAsync(int loanId);
    Task<List<Loan>> GetLoansByMemberAsync(int memberId);
    Task<List<Loan>> GetOverdueLoansAsync();
}
