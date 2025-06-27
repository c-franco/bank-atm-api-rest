using BankATM.Domain;
using BankATM.Repository.Interface.Base;

namespace BankATM.Repository.Interface
{
    public interface IBankRepository : IBaseRepository<BankAccount>
    {
        Task<BankAccount?> GetByAccountNumber(string accountNumber);
    }
}
