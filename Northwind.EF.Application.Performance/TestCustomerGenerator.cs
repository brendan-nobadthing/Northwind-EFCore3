using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Northwind.EF.Application.Performance.Fakes;
using Northwind.EF.Domain.Entities;

namespace Northwind.EF.Application.Performance
{
    public class TestCustomerGenerator
    {

        public IEnumerable<Customer> Generate()
        {
            int id = 0;

            Randomizer.Seed = new Random(1234);

            var customerFaker = new Faker<Customer>()
                .RuleFor(t => t.CustomerId, f => "T" + (++id))
                .RuleFor(t => t.CompanyName, f => f.Company.CompanyName())
                .RuleFor(t => t.ContactName, f => f.Name.FullName())
                .RuleFor(t => t.Address, f => f.Address.StreetAddress());

            return customerFaker.Generate(9999);

        }

    }
}
