using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Northwind.EF.Domain.Entities;
using Shouldly;
using Xunit;

namespace Northwind.EF.Application.Performance
{
    public class ConfigTests
    {

        [Fact]
        public void WeHaveDbContext()
        {
            TestContext.Instance.NorthwindDbContext.ShouldNotBeNull("expected a dbcontext object");
        }



        [Fact]
        public void DbContextHasSomeCustomers()
        {
            TestContext.Instance.NorthwindDbContext.Set<Customer>().Any().ShouldBeTrue("expected some customers in the DB");
        }
    }
}
