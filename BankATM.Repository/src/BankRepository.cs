using BankATM.Database.Context;
using BankATM.Domain;
using BankATM.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankATM.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly AppDbContext _context;

        public BankRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<BankAccount>> GetAll()
        {
            return await _context.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount?> GetByAccountNumber(string accountNumber)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
