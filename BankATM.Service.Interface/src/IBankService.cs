using BankATM.Common.DTO;
using BankATM.Domain;

namespace BankATM.Service.Interface
{
    public interface IBankService
    {
        Task<List<BankAccountResponseDTO>> GetAllAccounts();
        Task<BankAccountResponseDTO> GetByAccountNumber(string accountNumber);
        Task Deposit(DepositRequestDTO request);
        Task Withdraw(WithdrawRequestDTO request);
    }
}
