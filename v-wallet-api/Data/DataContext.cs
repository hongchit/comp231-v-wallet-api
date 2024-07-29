using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using v_wallet_api.Models;

namespace v_wallet_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<FinancialTransaction> FinancialTransaction { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("tbl_UserAccount")
                .Property(userAccount => userAccount.AccountType)
                .HasConversion(new EnumToStringConverter<AccountType>());

            modelBuilder.Entity<UserProfile>()
                .ToTable("tbl_UserProfile");
        }
    }
}
