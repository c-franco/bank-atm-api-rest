using BankATM.Common.Constants;
using BankATM.Common.DTO;
using BankATM.Domain;
using BankATM.Repository.Interface;
using BankATM.Service.Interface;

namespace BankATM.Service
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repository;

        public BankService(IBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BankAccount>> GetAllAccounts()
        {
            List<BankAccount> accounts = await _repository.GetAll();
            
            return accounts;
        }

        public async Task<BankAccount> GetByAccountNumber(string accountNumber)
        {
            BankAccount account = await _repository.GetByAccountNumber(accountNumber);

            if (account == null)
                throw new KeyNotFoundException(GlobalErrors.AccountNotFound);

            return account;
        }

        public async Task Deposit(DepositRequestDTO request)
        {
            BankAccount account = await GetByAccountNumber(request.AccountNumber);

            account.Deposit(request.Amount);
            await _repository.SaveAsync();
        }

        public async Task Withdraw(WithdrawRequestDTO request)
        {
            BankAccount account = await GetByAccountNumber(request.AccountNumber);

            account.Withdraw(request.Amount);
            await _repository.SaveAsync();
        }
    }
}
