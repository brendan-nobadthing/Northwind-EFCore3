using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.EF.Persistence.PSQL;

namespace Northwind.EF.Persistence.MSSQL
{
    public static class PsqlServiceCollectionExtensions
    {
        public static IServiceCollection AddPsqlDbContext(
            this IServiceCollection serviceCollection, 
            IConfiguration config = null)
        {
            serviceCollection.AddDbContext<NorthwindDbContext, PsqlNorthwindDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("NorthwindDbContext"), b => b.MigrationsAssembly("Northwind.EF.Persistence.PSQL"));
            });
            return serviceCollection;
        }
    }
}
