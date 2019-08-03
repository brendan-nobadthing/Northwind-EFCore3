using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.EF.Persistence.MSSQL;

namespace Northwind.EF.WebApi
{
    public static class PersistenceServiceCollectionExtensions
    {

        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration config = null)
        {
            var persistenceConfig = config?.GetSection("Persistence")?.Get<PersistenceConfiguration>();

            if (persistenceConfig?.Provider == "MSSQL")
            {
                serviceCollection.AddMssqlDbContext(config);
            }
            if (persistenceConfig?.Provider == "PSQL")
            {
                serviceCollection.AddPsqlDbContext(config);
            }

            return serviceCollection;
        }

    }

    public class PersistenceConfiguration
    {
        public string Provider { get; set; }
    }
}
