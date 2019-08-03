using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Northwind.EF.Application.Interfaces;

namespace Northwind.EF.Persistence
{
    public class NorthwindDbContext: DbContext, INorthwindDbContext
    {
       
        public NorthwindDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
        }

      
    }
}
