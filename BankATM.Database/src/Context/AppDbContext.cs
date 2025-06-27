using Microsoft.EntityFrameworkCore;
using BankATM.Domain;

namespace BankATM.Database.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().HasKey(b => b.Id);
            modelBuilder.Entity<Person>().HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
