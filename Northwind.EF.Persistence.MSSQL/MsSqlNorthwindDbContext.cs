using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Persistence;

namespace Northwind.EF.Persistence.MSSQL
{
    public class MsSqlNorthwindDbContext : NorthwindDbContext
    {
        public MsSqlNorthwindDbContext(DbContextOptions<NorthwindDbContext> options): base(options)
        {
        }
    }
}
