namespace MyLibraryApp.Models;

public class Member : Person
{
    public DateTime MembershipDate { get; set; }
    public int MaxBooksAllowed { get; set; } = 3;

    public List<Loan> Loans { get; set; } = new();

    public override string GetContactInfo()
    {
        return $"Member {Name} ({Email}), joined {MembershipDate:d}";
    }
}
