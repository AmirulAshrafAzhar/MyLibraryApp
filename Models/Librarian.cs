namespace MyLibraryApp.Models;

public class Librarian : Person
{
    public string EmployeeId { get; set; } = string.Empty;

    public override string GetContactInfo()
    {
        return $"Librarian {Name} ({Email}), staff id {EmployeeId}";
    }
}
