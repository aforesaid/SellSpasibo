using System.Threading;
using System.Threading.Tasks;

namespace SellSpasibo.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}