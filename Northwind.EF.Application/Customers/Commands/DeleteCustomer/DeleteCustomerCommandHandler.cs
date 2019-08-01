using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.EF.Application.Exceptions;
using Northwind.EF.Application.Interfaces;
using Northwind.EF.Domain.Entities;

namespace Northwind.EF.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly INorthwindDbContext _context;

        public DeleteCustomerCommandHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Customer>()
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            var hasOrders = _context.Set<Order>().Any(o => o.CustomerId == entity.CustomerId);
            if (hasOrders)
            {
                // TODO: Add functional test for this behaviour.
                throw new DeleteFailureException(nameof(Customer), request.Id, "There are existing orders associated with this customer.");
            }

            _context.Set<Customer>().Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
