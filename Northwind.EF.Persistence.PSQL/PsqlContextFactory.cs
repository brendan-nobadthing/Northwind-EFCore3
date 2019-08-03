using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.EF.Persistence.PSQL
{
    public class PsqlContextFactory : IDesignTimeDbContextFactory<PsqlNorthwindDbContext>
    {
        public PsqlNorthwindDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            var builder = new DbContextOptionsBuilder<NorthwindDbContext>();
            builder.UseNpgsql(
                config.GetConnectionString("MSSQL"), 
                b => b.MigrationsAssembly("Northwind.EF.Persistence.PSQL")
            );
            return new PsqlNorthwindDbContext(builder.Options);
        }
    }
}
