using Microsoft.AspNetCore.Mvc;
using MyLibraryApp.Services;

namespace MyLibraryApp.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromQuery] int memberId, [FromQuery] int bookId)
    {
        var result = await _loanService.BorrowBookAsync(memberId, bookId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Loan);
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> Return(int id)
    {
        var result = await _loanService.ReturnBookAsync(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [HttpGet("member/{memberId}")]
    public async Task<IActionResult> GetByMember(int memberId)
    {
        return Ok(await _loanService.GetLoansByMemberAsync(memberId));
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue()
    {
        return Ok(await _loanService.GetOverdueLoansAsync());
    }
}
