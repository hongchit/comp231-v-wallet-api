using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
using v_wallet_api.Models;

namespace v_wallet_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<FinancialAccount> FinancialAccount { get; set; }
        public DbSet<FinancialAccountType> FinancialAccountType { get; set; }
        public DbSet<FinancialTransaction> FinancialTransaction { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Currency> Currency { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("tbl_UserAccount")
                .Property(userAccount => userAccount.AccountType)
                .HasConversion(new EnumToStringConverter<AccountType>());

            modelBuilder.Entity<UserProfile>()
                .ToTable("tbl_UserProfile");

            modelBuilder.Entity<FinancialAccount>()
                .ToTable("tbl_FinancialAccount");

            modelBuilder.Entity<FinancialTransaction>().ToTable("tbl_FinancialTransaction");

            modelBuilder.Entity<FinancialAccountType>().ToTable("tbl_FinancialAccountType");

            modelBuilder.Entity<Category>().ToTable("tbl_Category");
            modelBuilder.Entity<Currency>().ToTable("tbl_Currency");


        }
    }
}
