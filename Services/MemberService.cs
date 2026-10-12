using Microsoft.EntityFrameworkCore;
using MyLibraryApp.Data;
using MyLibraryApp.Models;

namespace MyLibraryApp.Services;

public class MemberService : IMemberService
{
    private readonly LibraryContext _context;

    public MemberService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Member>> GetAllAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.FindAsync(id);
    }

    public async Task<Member> CreateAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<bool> UpdateAsync(int id, Member member)
    {
        var existing = await _context.Members.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = member.Name;
        existing.Email = member.Email;
        existing.MembershipDate = member.MembershipDate;
        existing.MaxBooksAllowed = member.MaxBooksAllowed;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Members.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _context.Members.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
