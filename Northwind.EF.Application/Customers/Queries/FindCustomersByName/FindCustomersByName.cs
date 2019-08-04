using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Application.Customers.Queries.GetCustomersList;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Entities;

namespace Northwind.EF.Application.Customers.Queries.FindCustomersByName
{
    public class FindCustomersByName : IRequest<CustomersListViewModel>
    {
        public string Name { get; }

        public FindCustomersByName(string name)
        {
            Name = name;
        }

        public class Handler : IRequestHandler<FindCustomersByName, CustomersListViewModel>
        {
            private readonly INorthwindDbContext _context;

            public Handler(INorthwindDbContext context)
            {
                _context = context;
            }

            public async Task<CustomersListViewModel> Handle(FindCustomersByName request, CancellationToken cancellationToken)
            {
                return new CustomersListViewModel
                {
                    Customers = await _context.Set<Customer>()
                        .Where(c => c.CompanyName.Contains(request.Name))
                        //.Where(c => c.CompanyName.StartsWith(request.Name))
                        //.Where(c => c.CompanyName.Equals(request.Name))
                        .Select(c => new CustomerLookupModel()
                        {
                            Name = c.CompanyName,
                            Id = c.CustomerId
                        })
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
