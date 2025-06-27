using BankATM.Common.DTO;
using BankATM.Domain;

namespace BankATM.Service.Interface
{
    public interface IBankService
    {
        Task<List<BankAccount>> GetAllAccounts();
        Task<BankAccount> GetByAccountNumber(string accountNumber);
        Task Deposit(DepositRequestDTO request);
        Task Withdraw(WithdrawRequestDTO request);
    }
}
