using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.EF.Application.Customers.Queries.GetCustomersList;
using Northwind.EF.Application.Test.Infrastructure;
using Northwind.EF.Persistence;
using Shouldly;
using Xunit;

namespace Northwind.EF.Application.Test.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomersListQueryHandlerTests
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            var sut = new GetCustomersListQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            result.ShouldBeOfType<CustomersListViewModel>();

            result.Customers.Count.ShouldBe(3);
        }
    }
}