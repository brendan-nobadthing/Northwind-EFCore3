using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.EF.Application.Exceptions;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Entities;

namespace Northwind.EF.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailModel>
    {
        private readonly INorthwindDbContext _context;

        public GetCustomerDetailQueryHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDetailModel> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Customer>()
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            return CustomerDetailModel.Create(entity);
        }
    }
}
