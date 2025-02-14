using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> _loanRepository;

        public LoanService(IRepository<Loan> loanRepository)
        {
            _loanRepository = loanRepository;
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
            await _loanRepository.AddAsync(loan);
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
