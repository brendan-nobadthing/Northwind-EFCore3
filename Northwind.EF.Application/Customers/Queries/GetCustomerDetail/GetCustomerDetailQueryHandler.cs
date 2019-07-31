using System.Threading;
using System.Threading.Tasks;
using Northwind.EF.Application.Exceptions;

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
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            return CustomerDetailModel.Create(entity);
        }
    }
}
