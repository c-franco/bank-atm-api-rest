using BankATM.Database.Context;
using BankATM.Domain;

namespace BankATM.Database.src.Context
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Persons.Any())
            {
                Person person = new Person { DNI = "11111111A", Name = "Christian", Surname = "Franco" };

                BankAccount account1 = new BankAccount { AccountNumber = "ES1920956893611111113923", Entity = "BBVA", Balance = 1251.74m, Person = person };
                BankAccount account2 = new BankAccount { AccountNumber = "ES6420386343135175761749", Entity = "BBVA", Balance = 438.61m, Person = person };

                context.Persons.Add(person);
                context.BankAccounts.AddRange(account1, account2);

                context.SaveChanges();
            }
        }
    }
}
