using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EF.Application.Interfaces
{
    public interface INorthwindDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
