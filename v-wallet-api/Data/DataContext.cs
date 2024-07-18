﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("tbl_UserAccount")
                .Property(userAccount => userAccount.AccountType)
                .HasConversion(new EnumToStringConverter<AccountType>());

        }
    }
}
