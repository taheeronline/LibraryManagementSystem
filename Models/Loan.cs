using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryManagementSystem.Models;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int MemberId { get; set; }
    public Member Member { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }=DateTime.Now.AddDays(7);
    public DateTime? ReturnDate { get; set; } = null;
}
