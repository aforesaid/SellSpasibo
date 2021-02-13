using Microsoft.EntityFrameworkCore;
using SellSpasibo.DAL.Entities;
namespace SellSpasibo.DAL
{
    public sealed class SellSpasiboDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public SellSpasiboDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
