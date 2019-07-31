using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EF.Persistence.PSQL
{
    public class PsqlNorthwindDbContext : NorthwindDbContext
    {

        public PsqlNorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {
        }

    }
}
