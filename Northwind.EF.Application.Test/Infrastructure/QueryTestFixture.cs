using System;
using AutoMapper;
using Northwind.EF.Persistence;
using Xunit;

namespace Northwind.EF.Application.Test.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public NorthwindDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = NorthwindContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            NorthwindContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}