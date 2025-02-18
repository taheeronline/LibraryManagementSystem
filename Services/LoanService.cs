using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> _loanRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly LibraryDbContext _context;
        public LoanService(IRepository<Loan> loanRepository, IRepository<Book> bookRepository, LibraryDbContext context)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepository.GetAllAsync(b=>b.Book, m=>m.Member);
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            return await _loanRepository.GetByIdAsync(id, b => b.Book,m=>m.Member);
        }

        public async Task AddLoanAsync(Loan loan)
        {
            // Start a transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Add the loan
                    await _loanRepository.AddAsync(loan);

                    // Get the book and update availability
                    var book = await _bookRepository.GetByIdAsync(loan.BookId);
                    book.IsAvailable = false;
                    await _bookRepository.UpdateAsync(book);

                    // Commit the transaction if everything is successful
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Rollback the transaction if anything goes wrong
                    await transaction.RollbackAsync();
                    throw; // Re-throw the exception to handle it elsewhere if needed
                }
            }
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            await _loanRepository.UpdateAsync(loan);
        }

        public async Task DeleteLoanAsync(int id)
        {
            await _loanRepository.DeleteAsync(id);
        }
    }
}
