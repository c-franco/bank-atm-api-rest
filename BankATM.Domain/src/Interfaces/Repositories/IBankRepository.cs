using BankATM.Domain.Interfaces.Repositories.Base;
using BankATM.Domain.Entities;

namespace BankATM.Domain.Interfaces.Repositories
{
    public interface IBankRepository : IBaseRepository<BankAccount>
    {
        Task<BankAccount?> GetByAccountNumber(string accountNumber);
    }
}
