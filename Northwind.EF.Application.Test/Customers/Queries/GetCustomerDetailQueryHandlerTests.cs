using System.Threading;
using System.Threading.Tasks;
using Northwind.EF.Application.Customers.Queries.GetCustomerDetail;
using Northwind.EF.Application.Test.Infrastructure;
using Northwind.EF.Persistence;
using Shouldly;
using Xunit;

namespace Northwind.EF.Application.Test.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerDetailQueryHandlerTests
    { 
        private readonly NorthwindDbContext _context;

        public GetCustomerDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }    

        public async Task GetCustomerDetail()
        {
            var sut = new GetCustomerDetailQueryHandler(_context);

            var result = await sut.Handle(new GetCustomerDetailQuery { Id = "JASON" }, CancellationToken.None);

            result.ShouldBeOfType<CustomerDetailModel>();
            result.Id.ShouldBe("JASON");
        }
    }
}
