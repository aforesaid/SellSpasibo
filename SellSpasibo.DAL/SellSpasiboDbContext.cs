using Microsoft.EntityFrameworkCore;
using SellSpasibo.DAL.Entities;
namespace SellSpasibo.DAL
{
    public sealed class SellSpasiboDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bank> Banks { get; set; }

        public SellSpasiboDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        //TODO: добавить индексы
        //TODO: добавить миграции
        //TODO: следовать DDD
        //TODO: реализовать репозиторий
        //TODO: запечатать этот класс от обновлений (Unit Of Work)
    }
}
