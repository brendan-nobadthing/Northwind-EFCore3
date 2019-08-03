using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.EF.Persistence.MSSQL
{
    public class MsSqlContextFactory : IDesignTimeDbContextFactory<MsSqlNorthwindDbContext>
    {
        public MsSqlNorthwindDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            var builder = new DbContextOptionsBuilder<NorthwindDbContext>();
            builder.UseSqlServer(
                config.GetConnectionString(nameof(NorthwindDbContext)),
                b => b.MigrationsAssembly("Northwind.EF.Persistence.MSSQL")
            );
            return new MsSqlNorthwindDbContext(builder.Options);
        }
    }
}
