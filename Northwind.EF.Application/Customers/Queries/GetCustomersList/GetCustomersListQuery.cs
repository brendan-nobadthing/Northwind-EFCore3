using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Entities;

namespace Northwind.EF.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<CustomersListViewModel>
    {
        // no parameters


        public class Handler : IRequestHandler<GetCustomersListQuery, CustomersListViewModel>
        {
            private readonly INorthwindDbContext _context;
            private readonly IMapper _mapper;

            public Handler(INorthwindDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CustomersListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
            {
                return new CustomersListViewModel
                {
                    Customers = await _context.Set<Customer>().ProjectTo<CustomerLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                };
            }
        }

    }
}
