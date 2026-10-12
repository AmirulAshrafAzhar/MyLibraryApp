using MyLibraryApp.Models;

namespace MyLibraryApp.Services;

public interface IMemberService
{
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> CreateAsync(Member member);
    Task<bool> UpdateAsync(int id, Member member);
    Task<bool> DeleteAsync(int id);
}
