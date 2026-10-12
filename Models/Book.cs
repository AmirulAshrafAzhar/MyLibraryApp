namespace MyLibraryApp.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }

    public List<Loan> Loans { get; set; } = new();

    public bool BorrowCopy()
    {
        if (AvailableCopies <= 0)
        {
            return false;
        }

        AvailableCopies--;
        return true;
    }

    public void ReturnCopy()
    {
        if (AvailableCopies < TotalCopies)
        {
            AvailableCopies++;
        }
    }
}
