using BankATM.Domain.Entities.Base;
using BankATM.Domain.Constants;

namespace BankATM.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public string AccountNumber { get; set; }

        public string Entity { get; set; }

        public decimal Balance { get; set; }

        public Person Person { get; set; } = null!;

        public void Deposit(decimal amount)
        {
            if (amount > 3000)
                throw new InvalidOperationException(GlobalErrors.DepositLimitExceeded);

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 1000)
                throw new InvalidOperationException(GlobalErrors.WithdrawLimitExceeded);

            if (amount > Balance)
                throw new InvalidOperationException(GlobalErrors.InsufficientFunds);

            Balance -= amount;
        }
    }
}
