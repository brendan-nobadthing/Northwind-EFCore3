using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.EF.Persistence.MSSQL
{
    public static class MsSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddMssqlDbContext(
            this IServiceCollection serviceCollection, 
            IConfiguration config = null)
        {
            serviceCollection.AddDbContext<NorthwindDbContext, MsSqlNorthwindDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("NorthwindDbContext"), b => b.MigrationsAssembly("Northwind.EF.Persistence.MSSQL"));
            });
            return serviceCollection;
        }
    }
}
