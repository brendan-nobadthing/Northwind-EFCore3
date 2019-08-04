using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Northwind.EF.Application.Customers.Queries.FindCustomersByName;
using Shouldly;
using Xunit;

namespace Northwind.EF.Application.Performance
{
    public class CustomersByName
    {

        [Fact]
        public async Task CustomersByNamePerfTest()
        {
            var handler = new FindCustomersByName.Handler(TestContext.Instance.NorthwindDbContext);

            var timer = new Stopwatch();
            timer.Start();

            var result = handler.Handle(new FindCustomersByName("Alfreds Futterkiste"), CancellationToken.None);

            timer.Stop();
            timer.ElapsedMilliseconds.ShouldBeLessThan(20, "expected query to be faster");
        }



    }
}
