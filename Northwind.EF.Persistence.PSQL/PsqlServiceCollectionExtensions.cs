using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.EF.Persistence.PSQL;

namespace Northwind.EF.Persistence.MSSQL
{
    public static class MsSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddPsqlDbContext<TContext>(
            this IServiceCollection serviceCollection, 
            IConfiguration config = null)
            where TContext: DbContext
        {
            serviceCollection.AddDbContext<NorthwindDbContext, PsqlNorthwindDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("NorthwindDbContext"), b => b.MigrationsAssembly("Northwind.EF.Persistence.PSQL"));
            });
            return serviceCollection;
        }
    }
}
