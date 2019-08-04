using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Domain.Entities;
using Northwind.EF.Persistence;
using Northwind.EF.Persistence.MSSQL;

namespace Northwind.EF.Application.Performance
{
    /// <summary>
    /// ensure we only use one instance of the razor engine and pdf engine in our tests.
    /// at runtime these should be configured in DI as singletons
    ///
    /// https://csharpindepth.com/articles/singleton - #6
    /// </summary>
    public sealed class TestContext
    {
        /// <summary>
        /// access to our singleton instance
        /// </summary>
        public static TestContext Instance => Lazy.Value;


        public NorthwindDbContext NorthwindDbContext { get; }

        private static readonly Lazy<TestContext> Lazy =
            new Lazy<TestContext>(() => new TestContext());


        private TestContext()
        {
            var builder = new DbContextOptionsBuilder<NorthwindDbContext>();
            builder.UseSqlServer(
                "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=Northwind_loadtest;Integrated Security=SSPI;",
                b => b.MigrationsAssembly("Northwind.EF.Persistence.MSSQL"));

            // use a different dbcontext to perform setup
            using (var initContext = new MsSqlNorthwindDbContext(builder.Options))
            {
                initContext.Database.EnsureDeleted();
                initContext.Database.Migrate();
                NorthwindInitializer.Initialize(initContext);

                var fakeCustomers = new TestCustomerGenerator().Generate();

                // todo: try bulkinsert again after release of ef core 3
                //NorthwindDbContext.BulkInsert<Customer>(fakeCustomers.ToList());

                try
                {
                    initContext.Customers.AddRange(fakeCustomers);
                    initContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw ex.InnerException ?? ex;
                }
                
            }

            NorthwindDbContext = new MsSqlNorthwindDbContext(builder.Options);

        }

    }
}
