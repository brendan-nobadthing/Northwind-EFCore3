using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Northwind.EF.Application.Performance.Fakes;

namespace Northwind.EF.Application.Performance
{
    public class TestCustomerGenerator
    {

        public IEnumerable<CustomerFake> Generate()
        {
            int id = 0;

            var customerFaker = new Faker<CustomerFake>()
                .RuleFor(t => t.CustomerId, f => "T" + (++id))
                .RuleFor(t => t.CompanyName, f => f.Company.CompanyName())
                .RuleFor(t => t.ContactName, f => f.Person.FullName)
                .RuleFor(t => t.Address, f => f.Address.FullAddress());

            return customerFaker.Generate(9999);

        }

    }
}
