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

        #region Public methods

        public async Task<List<BankAccountResponseDTO>> GetAllAccounts()
        {
            List<BankAccount> accounts = await _repository.GetAll();

            List<BankAccountResponseDTO> result = MapBankAccountsToDTO(accounts);

            return result;
        }

        public async Task<BankAccountResponseDTO> GetByAccountNumber(string accountNumber)
        {
            BankAccount account = await _repository.GetByAccountNumber(accountNumber);

            if (account == null)
                throw new KeyNotFoundException(GlobalErrors.AccountNotFound);

            BankAccountResponseDTO result = MapBankAccountToDTO(account);

            return result;
        }

        public async Task Deposit(DepositRequestDTO request)
        {
            BankAccount account = await _repository.GetByAccountNumber(request.AccountNumber);

            account.Deposit(request.Amount);
            await _repository.SaveAsync();
        }

        public async Task Withdraw(WithdrawRequestDTO request)
        {
            BankAccount account = await _repository.GetByAccountNumber(request.AccountNumber);

            account.Withdraw(request.Amount);
            await _repository.SaveAsync();
        }

        #endregion

        #region Private methods

        private List<BankAccountResponseDTO> MapBankAccountsToDTO(List<BankAccount> accounts)
        {
            return accounts.Select(MapBankAccountToDTO).ToList();
        }

        private BankAccountResponseDTO MapBankAccountToDTO(BankAccount account)
        {
            return new BankAccountResponseDTO
            {
                AccountNumber = account.AccountNumber,
                Entity = account.Entity,
                Balance = account.Balance
            };
        }

        #endregion
    }
}
