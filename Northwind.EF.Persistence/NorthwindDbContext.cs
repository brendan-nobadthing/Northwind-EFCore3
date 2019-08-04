using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Entities;
using Northwind.EF.Domain.Models;

namespace Northwind.EF.Persistence
{
    public class NorthwindDbContext: DbContext, INorthwindDbContext
    {

        public NorthwindDbContext()
        {

        }
       
        public NorthwindDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);

            modelBuilder.Entity<CustomersMostPurchasedProducts>()
                .HasNoKey()
                .ToQuery(() => Set<CustomersMostPurchasedProducts>().FromSqlRaw(@"
            select 
            	c.CustomerID
            	, c.CompanyName
            	, p.ProductID
            	, p.ProductName
            	, qtyCounts.QuantityPurchased
            	from Customers c 
            	inner join
            		(select  
            			o.CustomerID
            			, od.ProductID 
            			,sum(od.Quantity) as QuantityPurchased
            		from [Order Details] od
            		inner join [Orders] o on od.OrderID = o.OrderID
            		group by o.CustomerID, od.ProductID)  qtyCounts on c.CustomerID = qtyCounts.CustomerID
            	inner join Products p on p.ProductID = qtyCounts.ProductID
            "));

        }

      
    }
}
