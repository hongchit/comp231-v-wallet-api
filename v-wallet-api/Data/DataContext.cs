using Microsoft.EntityFrameworkCore;
using v_wallet_api.Models;

namespace v_wallet_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Account> Account { get; set; }
    }
}
