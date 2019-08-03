using System;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Domain.Entities;
using Northwind.EF.Persistence;

namespace Northwind.EF.Application.Test.Infrastructure
{
    public class NorthwindContextFactory
    {
        public static NorthwindDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new NorthwindDbContext(options);

            context.Database.EnsureCreated();

            context.Set<Customer>().AddRange(new[] {
                new Customer { CustomerId = "ADAM", ContactName = "Adam Cogan" },
                new Customer { CustomerId = "JASON", ContactName = "Jason Taylor" },
                new Customer { CustomerId = "BREND", ContactName = "Brendan Richards" },
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(NorthwindDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}